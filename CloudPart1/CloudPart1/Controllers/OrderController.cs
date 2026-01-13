using CloudPart1.Models;
using CloudPart1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace CloudPart1.Controllers
{
    public class OrderController : Controller
    {

        private readonly TableStorageService _tableStorageService;
       private readonly FileShareService _fileShareService;
        private readonly QueueService _queueService;


        public OrderController(TableStorageService tableStorageService, FileShareService fileShareService, QueueService queueService)
        {
            _tableStorageService = tableStorageService;
             _fileShareService = fileShareService;
            _queueService=queueService;

        }

       
        public async Task<IActionResult> Index()
        {
            var items = await _tableStorageService.GetAllItemsAsync();
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrder(Order order, IFormFile file)
        {

            var email = User.FindFirstValue(ClaimTypes.Email);
            var customerProfile = await _tableStorageService.GetCustomerProfileByEmailAsync(email);
            var orders = await _tableStorageService.GetAllOrdersAsync();

            var item = await _tableStorageService.GetItemByIdAsync(order.ItemId);
            


            if (item == null)
            {
                return NotFound("Item not found.");
            }

            order.TotalPrice = item.Price;
            order.CustomerName = customerProfile.FirstName;
            order.ItemId = item.Item_Id;
            order.CustomerId = customerProfile.RowKey;
            order.ItemName = item.Name;
            order.PartitionKey="OrderPartition";
            order.RowKey=Guid.NewGuid().ToString();

            
           
                using (var stream = file.OpenReadStream())
                {
                    var fileUrl = await _fileShareService.UploadFileAsync(file.FileName, stream);
                    order.FileUrl = fileUrl;
                }






                await _tableStorageService.AddOrderAsync(order);
                return RedirectToAction("OrderConfirm");
            
        }
        public IActionResult OrderConfirm()
        {
            return View();
        }

       

        [HttpGet]
        public IActionResult Order(int itemId)
        {
            var model = new Order
            {
                ItemId = itemId

            };
            return View(model);
        }

        public async Task<IActionResult> Orders()
        {
            
            var customerId = User.FindFirstValue(ClaimTypes.Email);

            
            var orders = await _tableStorageService.GetOrdersByCustomerIdAsync(customerId);

            double totalPrice = 0;

            foreach (var cartItem in orders)
            {
                var item = await _tableStorageService.GetItemByIdAsync(cartItem.ItemId);
                if (item != null)
                {
                    totalPrice += item.Price;
                }
            }

            return View(orders);
        }


        public async Task<IActionResult> DownloadFile(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
            {
                return NotFound();
            }

           

            var fileName = fileUrl.Split('/').Last();
            fileName = Uri.UnescapeDataString(fileName);

            var stream = await _fileShareService.DownloadFileAsync(fileName);
            if (stream == null)
            {
                return NotFound();
            }

            return File(stream, "application/octet-stream", fileName);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return BadRequest("Invalid order ID.");
            }

            var partitionKey = "OrderPartition";

            
            var order = await _tableStorageService.GetOrderByIdAsync(partitionKey, orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            try
            {
                
                await _tableStorageService.DeleteOrderAsync(orderId);

                
                if (!string.IsNullOrEmpty(order.FileUrl))
                {
                    var fileName = order.FileUrl.Split('/').Last();
                    await _fileShareService.DeleteFileAsync(fileName);
                }

                return RedirectToAction("Orders"); 
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error deleting order: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            try
            {
                
                var orders = await _tableStorageService.GetAllOrdersAsync();

                if (orders.Count == 0)
                {
                    return RedirectToAction("Orders"); 
                }

                
                foreach (var order in orders)
                {
                    
                    var checkoutOrder = new CheckoutOrder
                    {
                        PartitionKey = order.PartitionKey,
                        RowKey = order.RowKey,
                        ItemName=order.ItemName,
                        CustomerName=order.CustomerName,
                        Address=order.Address,
                        PhoneNumber=order.PhoneNumber,
                        ItemId = order.ItemId,
                        CustomerId = order.CustomerId,
                        TotalPrice = order.TotalPrice,
                        FileUrl = order.FileUrl,
                        
                    };

                    await _tableStorageService.AddCheckoutOrderAsync(checkoutOrder);
                    string message = $"Order processed successfully {checkoutOrder.ItemId} {checkoutOrder.ItemName} {checkoutOrder.TotalPrice} {checkoutOrder.Timestamp}.";
                    await _queueService.SendOrderAsync(message);
                }

                
                foreach (var order in orders)
                {
                    await _tableStorageService.DeleteOrderAsync(order.RowKey);
                }
                
                return RedirectToAction("CheckoutOrders"); 
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error during checkout: {ex.Message}");
                return View("Error"); 
            }
        }

        public async Task<IActionResult> CheckoutOrders()
        {
            
            var customerId = User.FindFirstValue(ClaimTypes.Email);

            
            var orders = await _tableStorageService.GetCheckoutOrdersByCustomerIdAsync(customerId);


            return View(orders);
        }
    }
}

using Azure.Storage.Blobs;
using CloudPart1.Models;
using CloudPart1.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPart1.Controllers
{
    public class ItemsController : Controller
    {
        private readonly BlobService _blobService;
        private readonly TableStorageService _tableStorageService;
        private readonly QueueService _queueService;



        public ItemsController(BlobService blobService, TableStorageService tableStorageService, QueueService queueService)
        {
            _blobService= blobService;
            _tableStorageService= tableStorageService;
            _queueService=queueService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var item = await _tableStorageService.GetAllItemsAsync();
            return View(item);
        }

        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(Item item, IFormFile file)
        {
            if (file !=null)
            {
                using var stream = file.OpenReadStream();
                var imageUrl = await _blobService.UploadAsync(stream, file.FileName);
                item.ImageUrl=imageUrl;
               

            }

            var items = await _tableStorageService.GetAllItemsAsync(); 
            item.Item_Id = items.Any() ? items.Max(i => i.Item_Id) + 1 : 1;

            item.PartitionKey="ItemsPartition";
            item.RowKey=Guid.NewGuid().ToString();
            


            await _tableStorageService.AddItemAsync(item);
            string message = $"New Item Added To Inventory {item.Item_Id} {item.Name} {item.Price} {item.Timestamp}.";
            await _queueService.SendMessageToQueueAsync(message);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItemAsync(string partitionKey, string rowKey)
        {
            await _tableStorageService.DeleteItemAsync(partitionKey, rowKey);

        


            return RedirectToAction("Index");
        }

       
    }
}

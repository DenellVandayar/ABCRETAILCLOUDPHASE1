using CloudPart1.Models;
using CloudPart1.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPart1.Controllers
{
    public class ProfileController : Controller
    {
        private readonly TableStorageService _tableStorageService;

        public ProfileController(TableStorageService tableStorageService)
        {
            _tableStorageService = tableStorageService;
        }

        public async Task<IActionResult> EditProfile()
        {
            var userId = User.Identity.Name; 
            var profile = await _tableStorageService.GetEntityAsync<CustomerProfile>("CustomerProfiles", userId);
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(CustomerProfile profile)
        {
            if (ModelState.IsValid)
            {
                await _tableStorageService.AddOrUpdateEntityAsync(profile);
                return RedirectToAction("ProfileUpdated");
            }
            return View(profile);
        }
    }
}

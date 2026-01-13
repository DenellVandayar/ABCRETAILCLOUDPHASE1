using CloudPart1.Areas.Identity.Data;
using CloudPart1.Models;
using CloudPart1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CloudPart1.Areas.Identity.Pages.Profile
{
    public class EditModel : PageModel
    {
        private readonly TableStorageService _tableStorageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(TableStorageService tableStorageService, UserManager<ApplicationUser> userManager)
        {
            _tableStorageService = tableStorageService;
            _userManager = userManager;
        }

        [BindProperty]
        public CustomerProfile Profile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            Profile = await _tableStorageService.GetEntityAsync<CustomerProfile>("CustomerProfiles", user.Email);
            if (Profile == null)
            {
                return NotFound("Profile not found.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _tableStorageService.AddOrUpdateEntityAsync(Profile);

            return RedirectToPage("/Index");
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using VroomAPI.Services.RentersService.RentersDTO;

namespace VroomWeb.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegistrationModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet() { }

        // OnPostAsync is now empty.  All logic is handled in JavaScript
        public async Task<IActionResult> OnPostAsync() { return Page(); }
    }
}
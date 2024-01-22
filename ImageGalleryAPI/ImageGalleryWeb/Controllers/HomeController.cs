using ImageGalleryWeb.Extensions;
using ImageGalleryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;

namespace ImageGalleryWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public  async Task<IActionResult> Index()
       {
            
            HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Sending request to find web api REST service resource GetAllEmployees using HttpClient
            HttpResponseMessage Res = await client.GetAsync("https://localhost:7095/Photos");
            var Results = Res.Content.ReadAsStringAsync().Result;
            var photos = JsonSerializer.Deserialize<IEnumerable<Photo>>(Results);
            return View(photos);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Upload() 
        {
            Photo photo = new();
            return View("UploadFormModal",photo);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(Photo photo )
        {
            HttpClient client = new();
            IFormFile file = Request.Form.Files[0];
            photo.UploadedImage = file.ConvertToBytes();
            var stringContent = new StringContent(JsonSerializer.Serialize(photo), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:7095/Photos", stringContent);

            return View(response);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
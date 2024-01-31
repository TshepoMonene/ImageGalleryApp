using ImageGalleryWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageGalleryWeb.Controllers
{
    public class PhotosController : Controller
    {
        public string baseUrl = "https://localhost:7096";

        [HttpPost]
        public IActionResult getPhoto(string id)
        {
            var results = id;
            return View("detailPage");
        }
    }
}

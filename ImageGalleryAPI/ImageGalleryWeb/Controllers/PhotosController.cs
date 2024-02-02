using ImageGalleryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ImageGalleryWeb.Controllers
{
    public class PhotosController : Controller
    {
        public string baseUrl = "https://localhost:7096";

        [HttpPost]
        public  async Task<IActionResult> getPhoto(string id)
        {
            Photo? photo = new();
            HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = new();
            try
            {
                 response = await client.GetAsync(baseUrl + "/getPhoto/"+id);
            }
            catch (Exception ex) 
            {
            
            }

            var Results = response.Content.ReadAsStringAsync().Result;
            if (Results != null)
            {
                photo = JsonSerializer.Deserialize<Photo>(Results);
                return View("detailPage",photo);
            }

            return NotFound();
            
        }

        [HttpPost]
        public async Task<IActionResult> update(Photo photo)
        {
            
            HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

           
            HttpResponseMessage response = new();
            try
            {

                var stringContent = new StringContent(JsonSerializer.Serialize(photo), System.Text.Encoding.UTF8, "application/json");
                response = await client.PutAsync(baseUrl + "/edit/",stringContent );
            }
            catch (Exception ex)
            {

            }

            var Results = response.Content.ReadAsStringAsync().Result;
            if (Results != null)
            {
                photo = JsonSerializer.Deserialize<Photo>(Results);
                return View("detailPage", photo);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            Photo? photo = new();
            HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = new();
            try
            {
                response = await client.GetAsync(baseUrl + "/getPhoto/" + id);
                var Results = response.Content.ReadAsStringAsync().Result;
                if (Results != null)
                {
                    photo = JsonSerializer.Deserialize<Photo>(Results);
                    var stringContent = new StringContent(JsonSerializer.Serialize(photo), System.Text.Encoding.UTF8, "application/json");
                    response = await client.PostAsync (baseUrl + "/delete/",stringContent);

                    if(response.IsSuccessStatusCode) 
                    {
                       return RedirectToAction("Index", "Home");    
                    }
                  
                }
            }
            catch (Exception ex)
            {

            }

            
       
            return NotFound();

        }
    }
}

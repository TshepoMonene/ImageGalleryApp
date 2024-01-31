
using ImageGalleryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ImageGalleryWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Login(LoginModel login) 
        {
            using(var client  = new HttpClient()) 
            {
                var stringContent = new StringContent(JsonSerializer.Serialize(login), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://localhost:7096/api/Login", stringContent);

                string token = await response.Content.ReadAsStringAsync();
                if (token == "User not found") 
                {
                    ViewBag.Message = "Invalid Username or PassWord";
                    return View();
          
                }

                HttpContext.Session.SetString("JWToken", token);
                
            }

            return RedirectToAction("Index","Home");
        }
    }
}

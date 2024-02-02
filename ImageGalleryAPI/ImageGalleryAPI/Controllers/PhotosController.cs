using ImageGalleryAPI.Contracts;
using ImageGalleryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageGalleryAPI.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PhotosController:ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _config;
        
        public PhotosController(IRepositoryManager repositoryManager,IConfiguration config)
        {
            _repositoryManager = repositoryManager;
             _config = config;
        }


        [HttpGet("/getPhotos")]
        public IActionResult GetAll()
        {
            return Ok(_repositoryManager.Photos.GetAll());
        }

        [HttpGet("/getPhoto/{id}")]
        public IActionResult GetPhoto(int id)
        {
            return Ok(_repositoryManager.Photos.Get(id));
        }

        [HttpPost("/upload")]
        public IActionResult Upload(Photo photo)
        {
            _repositoryManager.Photos.Create(photo);

            return Ok(_repositoryManager.Save());
        }

        [HttpPut("/edit")]
        public IActionResult Update(Photo photo) 
        {
            _repositoryManager.Photos.Update(photo);
            var results = _repositoryManager.Save();
            if(results > 0)
            {
                return Ok(photo);
            }
            return NotFound(); ;
        }
        [HttpPost("/delete")]
        public IActionResult Delete(Photo photo) 
        {
            _repositoryManager.Photos.Delete(photo);
            return Ok(_repositoryManager.Save());
        }
    }
}

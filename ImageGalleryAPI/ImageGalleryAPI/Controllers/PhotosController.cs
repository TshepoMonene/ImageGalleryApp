using ImageGalleryAPI.Contracts;
using ImageGalleryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageGalleryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotosController:ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public PhotosController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repositoryManager.Photos.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetPhoto(int id)
        {
            return Ok(_repositoryManager.Photos.Get(id));
        }

        [HttpPost]
        public IActionResult Upload(Photo photo)
        {
            _repositoryManager.Photos.Create(photo);

            return Ok(_repositoryManager.Save());
        }

        [HttpPut]
        public IActionResult Update(Photo photo) 
        {
            _repositoryManager.Photos.Update(photo);

            return Ok(_repositoryManager.Save());
        }
        [HttpDelete]
        public IActionResult Delete(Photo photo) 
        {
            _repositoryManager.Photos.Delete(photo);
            return Ok(_repositoryManager.Save());
        }
    }
}

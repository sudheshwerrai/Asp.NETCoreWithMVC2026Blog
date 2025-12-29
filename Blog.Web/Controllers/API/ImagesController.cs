using Blog.Web.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.NetworkInformation;

namespace Blog.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ICloudinaryImageRepository _imageRepository;
        public ImagesController(ICloudinaryImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsynch(IFormFile file)
        {
            var imageUrl = await _imageRepository.UploadAsync(file);
            return imageUrl == null
                ? Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError)
                : new JsonResult(new { link = imageUrl });
        }
    }
}



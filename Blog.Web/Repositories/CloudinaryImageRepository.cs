using Blog.Web.Repositories.IRepository;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

namespace Blog.Web.Repositories
{
    public class CloudinaryImageRepository : ICloudinaryImageRepository
    {
        private readonly IConfiguration _configuration = null;
        private readonly Account _account = null;
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var section = _configuration.GetSection("Cloudinary");
            _account = new Account(
                section["CloudName"],
                section["ApiKey"],
                section["ApiSecret"]);
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }

            return null;
        }
    }
}

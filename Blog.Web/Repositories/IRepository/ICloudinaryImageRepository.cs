namespace Blog.Web.Repositories.IRepository
{
    public interface ICloudinaryImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}

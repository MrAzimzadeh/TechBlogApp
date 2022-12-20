namespace WebApp.Helpers
{
    public static class ImageHelper
    {
        public static string UploadSinglePhoto(IFormFile image , IWebHostEnvironment env)
        {
            var path = "/uploads/" + Guid.NewGuid() + image.FileName;
            using (var fileStream = new FileStream(env.WebRootPath + path, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return path;
        }
    }
}

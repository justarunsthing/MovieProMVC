using MovieProMVC.Interfaces;

namespace MovieProMVC.Services
{
    public class ImageService : IImageService
    {
        public string DecodeImage(byte[] poster, string contentType)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> EncodeImageAsync(IFormFile poster)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> EncodeImageUrlAsync(string imageUrl)
        {
            throw new NotImplementedException();
        }
    }
}

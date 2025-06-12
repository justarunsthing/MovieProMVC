using MovieProMVC.Interfaces;

namespace MovieProMVC.Services
{
    public class ImageService : IImageService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ImageService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<byte[]> EncodeImageAsync(IFormFile poster)
        {
            if (poster == null)
                return null;

            using var ms = new MemoryStream();
            await poster.CopyToAsync(ms);

            return ms.ToArray();
        }

        public Task<byte[]> EncodeImageUrlAsync(string imageUrl)
        {
            throw new NotImplementedException();
        }

        public string DecodeImage(byte[] poster, string contentType)
        {
            throw new NotImplementedException();
        }
    }
}

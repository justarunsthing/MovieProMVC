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

        public async Task<byte[]> EncodeImageUrlAsync(string imageUrl)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(imageUrl);

            using Stream stream = await response.Content.ReadAsStreamAsync();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            return ms.ToArray();
        }

        public string DecodeImage(byte[] poster, string contentType)
        {
            throw new NotImplementedException();
        }
    }
}

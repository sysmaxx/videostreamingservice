using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using VideoStreamingService.Repositories;

namespace VideoStreamingService.Services
{
    public class StreamingService : IStreamingService
    {

        private readonly HttpClient client;
        private readonly IVideoRepository videoRepository;

        public StreamingService(IVideoRepository videoRepository)
        {
            this.client = new HttpClient();
            this.videoRepository = videoRepository;
        }

        public async Task<Stream> GetVideoStreamByNameAsync(string name) => await GetStreamAsync((await videoRepository.GetVideoByNameAsync(name)).URL);
        public async Task<Stream> GetVideoStreamByIDAsync(uint id) => await GetStreamAsync((await videoRepository.GetVideoByIDAsync(id)).URL);

        private async Task<Stream> GetStreamAsync(string url) => await client.GetStreamAsync(url);

        public void Dispose()
        {
            if (client is not null)
                client.Dispose();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using VideoStreamingService.Models;
using Extensions;
using VideoStreamingService.Exceptions;

namespace VideoStreamingService.Repositories
{
    public class VideoRepositoryIMM : IVideoRepository
    {

        private readonly HashSet<Video> videos = new()
        {
            new Video { ID = 0, Name = "earth", URL = "https://anthonygiretti.blob.core.windows.net/videos/earth.mp4" },
            new Video { ID = 1, Name = "nature1", URL = "https://anthonygiretti.blob.core.windows.net/videos/nature1.mp4" },
            new Video { ID = 2, Name = "nature2", URL = "https://anthonygiretti.blob.core.windows.net/videos/nature2.mp4" },
            new Video { ID = 3, Name = "notavailable", URL = "https://anthonygiretti.blob.core.windows.net/videos/notavailable.mp4" },
        };

        public async Task<Video> GetVideoByIDAsync(uint id)
        {
            return await Task.FromResult(videos.SingleOrElse(video => video.ID == id, () => throw new VideoNotFoundException($"Video ID: {id} not found")));
        }

        public async Task<Video> GetVideoByNameAsync(string name)
        {
            return await Task.FromResult(videos.SingleOrElse(video => video.Name == name, () => throw new VideoNotFoundException($"Video Name: {name} not found")));
        }


    }
}

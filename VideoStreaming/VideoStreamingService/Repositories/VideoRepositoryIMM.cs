using System.Collections.Generic;
using System.Threading.Tasks;
using VideoStreamingService.Models;
using Extensions;
using VideoStreamingService.Exceptions;
using System.Threading;

namespace VideoStreamingService.Repositories
{
    public class VideoRepositoryIMM : IVideoRepository
    {

        private readonly HashSet<Video> videos = new()
        {
            new Video { ID = 0, Name = "Nordhausen", Path = @"E:\FINAL\0.mp4" },
            new Video { ID = 1, Name = "Deister", Path = @"E:\FINAL\1.mp4" },
        };

        public async Task<Video> GetVideoByIDAsync(uint id, CancellationToken cancellationToken)
        {
            return await Task.Run(
                () => videos.SingleOrElse(video => video.ID == id,
                    () => throw new VideoNotFoundException($"Video ID: {id} not found")),
                cancellationToken);
        }

        public async Task<Video> GetVideoByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await Task.Run(
                () => videos.SingleOrElse(video => video.Name == name,
                    () => throw new VideoNotFoundException($"Video Name: {name} not found")),
                cancellationToken);
        }

    }
}

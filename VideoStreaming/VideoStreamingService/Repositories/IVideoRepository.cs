using System.Threading;
using System.Threading.Tasks;
using VideoStreamingService.Exceptions;
using VideoStreamingService.Models;

namespace VideoStreamingService.Repositories
{
    public interface IVideoRepository
    {
        Task<Video> GetVideoByNameAsync(string name, CancellationToken cancellationToken) => throw new VideoNotFoundException();
        Task<Video> GetVideoByIDAsync(uint id, CancellationToken cancellationToken) => throw new VideoNotFoundException();
    }
}

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace VideoStreamingService.Services
{
    public interface IStreamingService 
    {
        Task<Stream> GetVideoStreamByNameAsync(string name, CancellationToken cancellationToken);
        Task<Stream> GetVideoStreamByIDAsync(uint id, CancellationToken cancellationToken);
    }
}

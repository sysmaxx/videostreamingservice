using System;
using System.IO;
using System.Threading.Tasks;

namespace VideoStreamingService.Services
{
    public interface IStreamingService : IDisposable
    {
        Task<Stream> GetVideoStreamByNameAsync(string name);
        Task<Stream> GetVideoStreamByIDAsync(uint id);
    }
}

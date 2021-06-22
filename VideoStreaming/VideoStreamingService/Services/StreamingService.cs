using Microsoft.Extensions.Options;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VideoStreamingService.Models;
using VideoStreamingService.Repositories;

namespace VideoStreamingService.Services
{
    public class StreamingService : IStreamingService
    {

        private readonly IVideoRepository _videoRepository;
        private readonly IOptions<StreamingSettings> _options;

        public StreamingService(
            IVideoRepository videoRepository, 
            IOptions<StreamingSettings> options)
        {
            _videoRepository = videoRepository;
            _options = options;
        }

        public async Task<Stream> GetVideoStreamByIDAsync(uint id, CancellationToken cancellationToken)
        {
            var file = await _videoRepository.GetVideoByIDAsync(id, cancellationToken).ConfigureAwait(false);
            return GetVideoStream(file, cancellationToken);
        }

        public async Task<Stream> GetVideoStreamByNameAsync(string name, CancellationToken cancellationToken)
        {
            var file = await _videoRepository.GetVideoByNameAsync(name, cancellationToken).ConfigureAwait(false);
            return GetVideoStream(file, cancellationToken);
        }

        private FileStream GetVideoStream(Video file, CancellationToken cancellationToken)
        {
            var stream = GetFileStream(file.Path);

            RegisterDisposeStreamOnCancllation(stream, cancellationToken);
            return stream;
        }

        private FileStream GetFileStream(string path) => new(path, FileMode.Open, FileAccess.Read, FileShare.Read, _options.Value.BufferSize, FileOptions.Asynchronous | FileOptions.SequentialScan);

        private static void RegisterDisposeStreamOnCancllation(FileStream stream, CancellationToken cancellationToken)
        {
            cancellationToken.Register(async () =>
            {
                if (stream is not null)
                    await stream.DisposeAsync().ConfigureAwait(false);
            });
        }
    }
}

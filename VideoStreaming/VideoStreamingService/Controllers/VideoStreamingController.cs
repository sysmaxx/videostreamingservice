using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VideoStreamingService.Exceptions;
using VideoStreamingService.Services;

namespace VideoStreamingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoStreamingController : ControllerBase
    {
        private readonly IStreamingService _streamingService;
        private readonly ILogger<VideoStreamingController> _logger;

        public VideoStreamingController(IStreamingService videoStreamingService, ILogger<VideoStreamingController> logger)
        {
            _streamingService = videoStreamingService;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status206PartialContent, Type = typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("/{id:int}")]
        public async Task<IActionResult> GetStreamByIDAsync(uint id, CancellationToken cancellationToken) => 
            await TryGetFileStreamAsync(_streamingService.GetVideoStreamByIDAsync(id, cancellationToken)).ConfigureAwait(false);


        [ProducesResponseType(StatusCodes.Status206PartialContent, Type = typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("/{name}")]
        public async Task<IActionResult> GetStreamByNameAsync(string name, CancellationToken cancellationToken) =>
            await TryGetFileStreamAsync(_streamingService.GetVideoStreamByNameAsync(name, cancellationToken)).ConfigureAwait(false);
 


        private async Task<IActionResult> TryGetFileStreamAsync<T>(Task<T> task) 
            where T : Stream
        {
            try
            {
                return File(await task.ConfigureAwait(false), "application/octet-stream", "stream", true);
            }
            catch (VideoNotFoundException ex)
            {
                _logger.LogError($"{ex.Message}\n{ex.StackTrace}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}\n{ex.StackTrace}");
                return BadRequest();
            }
        }
    }
}

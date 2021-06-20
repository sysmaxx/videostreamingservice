using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VideoStreamingService.Exceptions;
using VideoStreamingService.Services;

namespace VideoStreamingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoStreamingController : ControllerBase
    {
        private readonly IStreamingService videoStreamingService;
        private readonly ILogger<VideoStreamingController> logger;

        public VideoStreamingController(IStreamingService videoStreamingService, ILogger<VideoStreamingController> logger)
        {
            this.videoStreamingService = videoStreamingService;
            this.logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("/{id:int}")]
        public async Task<IActionResult> GetStreamByIDAsync(uint id) => await TryGetStreamAsync(videoStreamingService.GetVideoStreamByIDAsync(id));

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("/{name}")]
        public async Task<IActionResult> GetStreamByNameAsync(string name) => await TryGetStreamAsync(videoStreamingService.GetVideoStreamByNameAsync(name));
 
        private async Task<IActionResult> TryGetStreamAsync<T>(Task<T> task)
        {
            try
            {
                return Ok(await task);
            }
            catch (VideoNotFoundException ex)
            {
                logger.LogError($"{ex.Message}\n{ex.StackTrace}");
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}\n{ex.StackTrace}");
                return BadRequest();
            }
        }
    }
}

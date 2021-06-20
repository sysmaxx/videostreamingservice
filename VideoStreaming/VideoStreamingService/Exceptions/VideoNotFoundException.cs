using System;

namespace VideoStreamingService.Exceptions
{
    public class VideoNotFoundException : Exception
    {
        public VideoNotFoundException()
        { }

        public VideoNotFoundException(string message)
           :base(message)
        { }
    }
}

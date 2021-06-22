using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoStreamingService.Models
{
    public class StreamingSettings
    {
        public int BufferSize { get; } = 65536;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoStreamingService.Models
{
    public class Video
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }
}

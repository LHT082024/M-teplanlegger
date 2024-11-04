using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Møteplanlegger.models
{
    public class Meeting
    {
        public string? Place { get; set; }

        public int MeetingId { get; set; }
        public string? Subject { get; set; }
        public int Attendees { get; set; }
    }
}
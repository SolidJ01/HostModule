using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostClientApplication.Models
{
    public class LocalBooking
    {
        public int Id { get; set; }
        public int StartDateTime { get; set; }
        public int EndDateTime { get; set; }
        public string Category { get; set; }
        public int LocalId { get; set; } // det här är foreign key
        public Local Locals { get; set; } // det här är foreign key

        public int EventId { get; set; } // Det här är foreign key
        public int HostId { get; set; } // Det här är foreign key
    }
}

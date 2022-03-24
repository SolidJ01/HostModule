using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostClientApplication.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public int EventId { get; set; }
    }
}

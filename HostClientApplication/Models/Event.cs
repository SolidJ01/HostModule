using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostClientApplication.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public int TicketsMax { get; set; }
        public int LocaleId { get; set; }
        public int HostId { get; set; }
        public string Picture { get; set; }
        public int VenueId { get; set; }
        public List<Booking> Booking { get; set; }

        // "Local" attributes
        public string BookedPercentage { get; set; }
    }
}

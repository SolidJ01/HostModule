using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostClientApplication.Models
{
    public class Local
    {

        public int Id { get; set; }
        
        public string Name { get; set; }
      
        public string ImgUrl { get; set; } = "";
      
        public string Description { get; set; }
       
        public string Ramp { get; set; } = "";
      
        public int MaxNumPeople { get; set; }
       
        public string Address { get; set; } = "";
       
        public int ZIPCode { get; set; }
      
        public string City { get; set; } = "";

        public string GPS { get; set; } = "";

        public List<LocalBooking> LocalBooking { get; set; } // foreign key
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostClientApplication.Models
{
    public class EventProducts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}

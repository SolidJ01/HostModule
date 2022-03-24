using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostClientApplication.Models
{
    public class ExtendedHost
    {
        public int LoginId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Website { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public int AccessLevel { get; set; }
        public int UserId { get; set; }



    }
}

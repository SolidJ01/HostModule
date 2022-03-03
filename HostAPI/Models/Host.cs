namespace HostAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Host
    {
        public int Id { get; set; }

        public int LoginId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Website { get; set; }
    }
}

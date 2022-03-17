using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostClientApplication.Models
{
    public class LocalContext : DbContext
    {

        public DbSet<LocalBooking> LocalBookings { get; set; }
        public DbSet<Local> Local { get; set; }


        public LocalContext(DbContextOptions options) : base(options)
        {

        }

    }



}


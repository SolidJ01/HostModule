using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostAPI.Models;

namespace HostAPI.Models
{
    public class HostContext : DbContext
    {
        public DbSet<Host> Hosts { get; set; }

        public HostContext(DbContextOptions options) : base (options)
        {
            
        }
    }
}

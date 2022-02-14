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
        DbSet<Host> Hosts;
    }
}

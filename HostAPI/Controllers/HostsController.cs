using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HostAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HostsController : ControllerBase
    {
        private readonly HostContext db;

        public HostsController(HostContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Host>> GetHosts()
        {
            return db.Hosts.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Host> GetHost(int id)
        {
            Host host = db.Hosts.Find(id);

            if (host == null)
            {
                return NotFound();
            }

            return host;
        }

        [HttpPut("{id}")]
        public ActionResult PutHost(int id, Host host)
        {
            if (id != host.Id) return BadRequest();
            if (!HostExists(id)) return NotFound();
            try
            {
                db.Entry(host).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        public ActionResult<Host> PostHost(Host host)
        {
            try
            {
                db.Hosts.Add(host);
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
            return CreatedAtAction("GetHost", new { id = host.Id }, host);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteHost(int id)
        {
            if (!HostExists(id)) return NotFound();
            try
            {
                db.Hosts.Remove(db.Hosts.Find(id));
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
            return Ok();
        }

        private bool HostExists(int id)
        {
            return db.Hosts.Any(e => e.Id == id);
        }
    }
}

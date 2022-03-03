using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostClientApplication.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;

namespace HostClientApplication.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        static HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            Host host = new Host();
            var response = await client.GetAsync("http://193.10.202.73/HostAPI/Hosts/" + int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            string jsonresponse = await response.Content.ReadAsStringAsync();
            host = JsonConvert.DeserializeObject<Host>(jsonresponse);

            return View(host);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Host host)
        {
            host.Id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            using (var response = await client.PutAsJsonAsync("http://193.10.202.73/HostAPI/Hosts/" + host.Id, host))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(apiResponse);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete()
        {
            HttpResponseMessage response = await client.DeleteAsync("http://193.10.202.73/HostAPI/Hosts/" + int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            Console.WriteLine("Account Deletion: " + response.StatusCode);

            return RedirectToAction("Logout", "Login");
        }
    }
}

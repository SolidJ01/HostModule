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
            ExtendedHost host = new ExtendedHost();
            var response = await client.GetAsync("http://193.10.202.73/HostAPI/Hosts/" + int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (response.IsSuccessStatusCode)
            {
                string jsonresponse = await response.Content.ReadAsStringAsync();
                Host h = JsonConvert.DeserializeObject<Host>(jsonresponse);
                host.UserId = h.Id;
                host.Name = h.Name;
                host.Phone = h.Phone;
                host.Description = h.Description;
                host.Website = h.Website;
            }
            else
            {
                ViewBag.ErrorMessage = "Could not retrieve profile";
            }

            // Get login details from grp 5, add them, to the thing, do some shit



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

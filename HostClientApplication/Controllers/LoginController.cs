using HostClientApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HostClientApplication.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string name, string password)
        {
            List<Host> hosts = new List<Host>();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://193.10.202.73/HostAPI/Hosts");
            string jsonresponse = await response.Content.ReadAsStringAsync();
            hosts = JsonConvert.DeserializeObject<List<Host>>(jsonresponse);

            foreach (Host host in hosts)
            {
                if (host.Name.Equals(name) && password.Equals("Password"))
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, host.Name));
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, host.Id.ToString()));

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));

                    return RedirectToAction("", "Home");
                }
            }

            ViewBag.Message = "Nåt gick fel";
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}

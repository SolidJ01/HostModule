using AdminClientApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AdminClientApplication.Controllers
{
    public class HomeController : Controller
    {
        private static HttpClient client = new HttpClient();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Host> host = new List<Host>();
            var response = await client.GetAsync("http://193.10.202.73/HostAPI/Hosts/");
            string jsonresponse = await response.Content.ReadAsStringAsync();
            host = JsonConvert.DeserializeObject<List<Host>>(jsonresponse);

            return View(host);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            Host host = new Host();
            var response = await client.GetAsync("http://193.10.202.73/HostAPI/Hosts/" + Id);
            string jsonresponse = await response.Content.ReadAsStringAsync();
            host = JsonConvert.DeserializeObject<Host>(jsonresponse);

           List<LoginDetails> login = new List<LoginDetails>();
            response = await client.GetAsync("http://193.10.202.75/LoginAPI/api/LoginDetails");
            jsonresponse = await response.Content.ReadAsStringAsync();
            login = JsonConvert.DeserializeObject<List<LoginDetails>>(jsonresponse);

            LoginDetails loginDetails = new LoginDetails();

            foreach (LoginDetails item in login)
            {
                if (item.UserId == Id)
                {
                    loginDetails = item;
                    break;
                }
            }

            ExtendedHost extendedHost = new ExtendedHost();
            extendedHost.UserId = host.Id;
            extendedHost.Name = host.Name;
            extendedHost.Phone = host.Phone;
            extendedHost.Description = host.Description;
            extendedHost.Website = host.Website;

            extendedHost.Username = loginDetails.Username;
            extendedHost.Password = loginDetails.Password;
            extendedHost.AccessLevel = loginDetails.AccessLevel;
            extendedHost.UserId = loginDetails.UserId;

            return View(extendedHost);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExtendedHost extendedHost)
        {
            //bryt ut host objekt och skicka till host Api
            Host host = new Host();
            host.Name = extendedHost.Name;
            host.Phone = extendedHost.Phone;
            host.Description = extendedHost.Description;
            host.Website = extendedHost.Website;

           
            using (var response = await client.PostAsJsonAsync("http://193.10.202.73/HostAPI/Hosts", host))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(apiResponse);
                    host = JsonConvert.DeserializeObject<Host>(apiResponse);
                }
                else
                {
                    ViewBag.ErrorMessage = "Kunde inte skapa profil";
                    return View();
                }

            }
            //Bryt ut logindetails objekt och ge userId från skapad host, skicka till login Api
            LoginDetails login = new LoginDetails();
            login.Username = extendedHost.Username;
            login.Password = extendedHost.Password;
            login.AccessLevel = extendedHost.AccessLevel;
            login.UserId = host.Id;


            using (var response = await client.PostAsJsonAsync("http://193.10.202.75/LoginAPI/api/LoginDetails", login))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(apiResponse);
                    login = JsonConvert.DeserializeObject<LoginDetails>(apiResponse);
                }
                else
                {
                    ViewBag.ErrorMessage = "Kunde inte skapa konto";
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

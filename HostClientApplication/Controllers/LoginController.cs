using HostClientApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

            try
            {
                var url = "http://193.10.202.75/LoginAPI/" + $"Authenticate?Username={name}&Password={password}";
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(url));

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Login Failed");
                    ViewBag.Message = "Fel Användarnamn och lösenord";
                    // return RedirectToAction("Index", "Login");
                    return View();
                }

                var content = await response.Content.ReadAsStringAsync();
                var recievedLogin = JsonConvert.DeserializeObject<LoginDetails>(content);
                Debug.WriteLine("Login Successful");
                Debug.WriteLine(recievedLogin.UserId);



                response = await httpClient.GetAsync("http://193.10.202.73/HostAPI/Hosts/"+ recievedLogin.UserId);
                content = await response.Content.ReadAsStringAsync();
                Host host = new Host();
                host = JsonConvert.DeserializeObject<Host>(content);

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, host.Name));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, host.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Sid, recievedLogin.AccessLevel.ToString()));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToAction("", "Home");

            }
            catch (Exception e)
            {
                Debug.WriteLine("Login failed: " + e.StackTrace);
                return null;
            }



        }

        public async Task<IActionResult> New()
        {
            ExtendedHost host = new ExtendedHost();
            return View(host);
        }
        [HttpPost]
        public async Task<IActionResult> New(ExtendedHost extendedHost)
        {
            //bryt ut host objekt och skicka till host Api
            Host host = new Host();
            host.Name = extendedHost.Name;
            host.Phone = extendedHost.Phone;
            host.Description = extendedHost.Description;
            host.Website = extendedHost.Website;

            HttpClient client = new HttpClient();
            using (var response = await client.PostAsJsonAsync("http://193.10.202.73/HostAPI/Hosts",  host))
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


            //Om allt lyckats logga in användare på de skapade kontot
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, host.Name));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, host.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Sid, login.AccessLevel.ToString()));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return RedirectToAction("", "Home");

        }



        /*public async Task<IActionResult> Index(string name, string password)
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
        }*/

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HostClientApplication.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace HostClientApplication.Controllers
{
    public class EventController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient cl = new HttpClient();
            List<Event> events = new List<Event>();
            var response = await cl.GetAsync("http://193.10.202.74/EventAPI1/api/Events");
            string jsonresponse = await response.Content.ReadAsStringAsync();
            events = JsonConvert.DeserializeObject<List<Event>>(jsonresponse).ToList();

            List<Event> goodevents = new List<Event>();
            Random rand = new Random();
            foreach (Event e in events)
            {
                if (e.HostId == int.Parse (User.FindFirstValue(ClaimTypes.NameIdentifier)))
                {
                    if (e.Booking == null)
                    {
                        e.Booking = new List<Booking>();
                        int roof = rand.Next(e.TicketsMax / 3, e.TicketsMax + 1);
                        for (int i = 0; i < roof; i++)
                        {
                            Booking booking = new Booking();
                            e.Booking.Add(booking);
                        }
                    }
                    e.BookedPercentage = (decimal.Divide(e.Booking.Count, e.TicketsMax) * 100).ToString().Replace(",", ".");
                    goodevents.Add(e);
                }
            }



            return View(goodevents);
        }

        public async Task<IActionResult> Products(int id)
        {
            HttpClient cl = new HttpClient();
            EventProducts eventProducts = new EventProducts();
            using (var response = await cl.GetAsync("http://193.10.202.74/EventAPI1/api/Events/" + id))
            {
                string jsonresponse = await response.Content.ReadAsStringAsync();
                Event ev = JsonConvert.DeserializeObject<Event>(jsonresponse);
                eventProducts.Id = ev.Id;
                eventProducts.Name = ev.Name;
            }

            using (HttpResponseMessage response = await cl.GetAsync("http://193.10.202.71/GroupOne_WebApi/api/Products/Event/" + id))
            {
                string jsonresponse = await response.Content.ReadAsStringAsync();
                try
                {
                    List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonresponse);
                    eventProducts.Products = products;
                }
                catch
                {
                    Console.WriteLine("Event has no products");
                    eventProducts.Products = new List<Product>();
                }
            }

            return View(eventProducts);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Detalis()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

    }
}

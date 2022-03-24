using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using HostClientApplication.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HostClientApplication.Controllers
{
    public class EventController : Controller
    {
        static HttpClient client = new HttpClient();
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


        // GET: Event/Create
        public async Task<IActionResult> Create()
        {
            List<Local> localList = new List<Local>();
            // hämtar från databasen
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    localList = JsonConvert.DeserializeObject<List<Local>>(apiResponse);

                }
                
            }
            ViewBag.LocalId = new SelectList(localList, "Id", "Name");
            Event e = new Event();
            return View(e);
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event eventFromView)
        {
            
                    
            using (var response = await client.PostAsJsonAsync("http://193.10.202.74/EventAPI1/api/Events/ ", eventFromView))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(apiResponse);
            }
            return RedirectToAction("Index");

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
            Event ed = new Event();
            return View(ed);
        }
        // POST: Event/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event content)
        {
            var jsonString = JsonConvert.SerializeObject(content);
            var contentJson = new StringContent(jsonString, Encoding.UTF8, "application/Json");

            using (var response = await client.PutAsync("http://193.10.202.74/EventAPI1/api/Events/", contentJson))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(apiResponse);
            }
            return RedirectToAction("Index");

        }

    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using HostClientApplication.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HostClientApplication.Controllers
{
    public class EventController : Controller
    {
        static HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            return View();
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

using HostClientApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HostClientApplication.Controllers
{
    public class LocalBookingController : Controller
    {
        

        // GET: LocalBookingController
        public async Task<IActionResult> Index()
        {
            List<LocalBooking> localbookingList = new List<LocalBooking>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    localbookingList = JsonConvert.DeserializeObject<List<LocalBooking>>(apiResponse);
                }
            }
            return View(localbookingList);

        }

       
        // GET: LocalBookingController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            LocalBooking localbooking = new LocalBooking();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    localbooking = JsonConvert.DeserializeObject<LocalBooking>(apiResponse);
                }
                return View(localbooking);
            }
        }
        // POST: LocalBookingController/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: LocalBookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(LocalBooking localbooking)
        {
            LocalBooking receivedLocalBooking = new LocalBooking();
            using (var httpClient = new HttpClient())
            {
                
                StringContent content = new StringContent(JsonConvert.SerializeObject(localbooking), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        receivedLocalBooking = JsonConvert.DeserializeObject<LocalBooking>(apiResponse);
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        ViewBag.Result = apiResponse;
                        return View();
                    }

                    return View(receivedLocalBooking);

                }
            }
        }
        // GET: LocalBookingController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            LocalBooking localbooking = new LocalBooking();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    localbooking = JsonConvert.DeserializeObject<LocalBooking>(apiResponse);
                }
                return View(localbooking);
            }
        }

        // POST: LocalBookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(LocalBooking localbooking)
        {
            LocalBooking receivedLocalBooking = new LocalBooking();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(localbooking), Encoding.UTF8, "application/json");


                using (var response = await httpClient.PutAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals/" + localbooking.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedLocalBooking = JsonConvert.DeserializeObject<LocalBooking>(apiResponse);
                }
            }
            return View(receivedLocalBooking);

        }


        // GET: LocalBookingController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            LocalBooking localObject = new LocalBooking();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    localObject = JsonConvert.DeserializeObject<LocalBooking>(apiResponse);
                }
            }
            return View(localObject);

        }

        // POST: LocalBookingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}


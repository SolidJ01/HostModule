using HostClientApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HostClientApplication.Controllers
{
    [Authorize]
    public class LocalController : Controller
    {
        // GET: LocalController
        string _baseURL = "http://193.10.202.72/LocalScheduleServiceAPI/api/Locals";
        public async Task<IActionResult> Index()
        {
            List<Local> localList = new List<Local>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    localList = JsonConvert.DeserializeObject<List<Local>>(apiResponse);
                }
            }
            return View(localList);

        }

        // GET: LocalController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            Local local = new Local();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    local = JsonConvert.DeserializeObject<Local>(apiResponse);
                }
                return View(local);
            }
        }

        // GET: LocalController/Create
        public ActionResult Create()
        {
            Local e = new Local();
            return View(e);
        }

        // POST: LocalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> CreateAsync(Local local)
        {
            Local receivedLocal = new Local();
            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(local), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        receivedLocal = JsonConvert.DeserializeObject<Local>(apiResponse);
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        ViewBag.Result = apiResponse;
                        return View();
                    }

                    return View(receivedLocal);

                }
            }
        }
        // GET: LocalController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Local local = new Local();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    local = JsonConvert.DeserializeObject<Local>(apiResponse);
                }
                return View(local);
            }
        }
        // POST: LocalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Local local)
        {
            Local receivedLocal = new Local();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(local), Encoding.UTF8, "application/json");


                using (var response = await httpClient.PutAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals/" + local.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedLocal = JsonConvert.DeserializeObject<Local>(apiResponse);
                }
            }
            return View(receivedLocal);

        }

        // GET: LocalController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Local localObject = new Local();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/Locals/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    localObject = JsonConvert.DeserializeObject<Local>(apiResponse);
                }
            }
            return View(localObject);

        }

        // POST: LocalController/Delete/5
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




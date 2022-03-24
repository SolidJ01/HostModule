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
    [ApiController]
    public class ConnectLocalBooking : ControllerBase
    {
        
        [Route("GetBookings")]
        [HttpGet]
        public async Task<List<LocalBooking>> GetLocalBookingsAsync(int lokalId, DateTime startdate, DateTime endDate)
        {
            List<LocalBooking> localbookingList = new List<LocalBooking>();
            // hämtar från databasen
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.72/LocalScheduleServiceAPI/api/LocalBookings/")) 
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    localbookingList = JsonConvert.DeserializeObject<List<LocalBooking>>(apiResponse);
                }
                return localbookingList;
            }

        }
    }
}

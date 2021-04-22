using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afisha.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Afisha.Controllers
{
    public class AdminSearchController : BaseController
    {
        public AdminSearchController(ILogger<HomeController> logger, AfishaContext _db) : base(logger, _db)
        { }
        public async Task<IActionResult> AdminSearchReservations()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AdminSearchReservations(int pay, AdminSearchVM inputGuid)
        {
            if (inputGuid.Guid != null)
            {
                var userReservations = await db.reservations.Where(g => g.Guid == inputGuid.Guid).ToListAsync();

                if (userReservations.Count == 0)
                {
                    TempData["ErrorSearch"] = $"Not found this hashCode";
                    return View(inputGuid);
                }

                int userId = 0;
                int seansId = 0;
                int totalPriceTicket = 0;
                List<int> seats = new List<int>{ };
                
                foreach (var item in userReservations)
                {
                    if (pay == 1) 
                    {
                        item.Status = pay;
                        await db.SaveChangesAsync();
                    } 
                    userId = item.UserId;
                    seansId = item.SeanseId;
                    seats.Add(item.SeatReservation);
                    if(item.Status == 0)
                    {
                        totalPriceTicket += item.Price;
                    }
                }
                var seans = await db.seanses.FirstOrDefaultAsync(c => c.Id == seansId);
                var concert = await db.concerts.FirstOrDefaultAsync(c => c.Id == seans.ConcertId);
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);


                var viewModel = new AdminSearchVM
                {
                    UserName = user.UserName,
                    TitleEvent = concert.TitleConcert,
                    Seats = seats,
                    Date = seans.Date,
                    TotalPrice = totalPriceTicket
                };
                return View(viewModel);
            }

            return View(inputGuid);
        }
    }
}

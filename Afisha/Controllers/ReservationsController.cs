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
    public class ReservationsController : BaseController
    {
        public ReservationsController(ILogger<HomeController> logger, AfishaContext _db) : base(logger, _db)
        { }

        [HttpGet]
        public async Task<IActionResult> ReservationsTicket(int Id, string date)
        {
            var dateEvent = DateTime.Parse(date);

            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            var SeansEvent = await db.seanses.FirstOrDefaultAsync(c => c.ConcertId == Id && c.Date == dateEvent);
            var concert = await db.concerts.FirstOrDefaultAsync(n => n.Id == Id);
            var location = await db.locations.FirstOrDefaultAsync(t => t.Id == concert.LocationId);
            var SeansesReservations = db.reservations.Where(n => n.SeanseId == SeansEvent.Id);
            
            List<int> seatsReservation = new List<int>(){ };
            foreach (var item in SeansesReservations)
            {
                seatsReservation.Add(item.SeatReservation);
            }
            
            var viewModel = new ReservationsVM
            {
                Id = concert.Id,
                Title = concert.TitleConcert,
                BeginPriceTicket = concert.PriceTicket,
                Seatreservations = seatsReservation,
                Date = dateEvent,
                TotalSeats = location.TotalSeats,
                UserName = user.UserName

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ReservationsTicket(ReservationsVM reservations, int[] orderNumbers,string date)
        {
            var dateEvent = DateTime.Parse(date);
            var seans = await db.seanses.FirstOrDefaultAsync(x => x.ConcertId == reservations.Id && x.Date == dateEvent);

            if (orderNumbers.Length != 0)
            {
                using (var transaction = await db.Database.BeginTransactionAsync())
                {
                    for (int i = 0; i < orderNumbers.Length; i++)
                    {
                        await db.reservations.AddAsync(new Reservation
                        {
                            SeanseId = seans.Id,
                            UserId = UserId,
                            SeatReservation = orderNumbers[i]
                        });
                    }
                    await db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = $"You have booked successfully!";
                    return RedirectToAction("ReservationsTicket", new { date = date });
                }
            }
            TempData["ErrorMessage"] = $"You have not selected a seat for booking";
            return RedirectToAction("ReservationsTicket", new { date = date } );
        }
    }
}

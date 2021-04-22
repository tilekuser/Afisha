using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afisha.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Afisha.Controllers
{
    public class ReservationsController : BaseController
    {
        public ReservationsController(ILogger<HomeController> logger, AfishaContext _db) : base(logger, _db)
        { }

        [HttpGet]
        public async Task<IActionResult> ReservationsTicketPhilharmonics(int Id, string date)
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
        public async Task<IActionResult> ReservationsTicketPhilharmonics(ReservationsVM reservations, List<int> orderNumbers,string date, string title)
        {
            var dateEvent = DateTime.Parse(date);
            var seans = await db.seanses.FirstOrDefaultAsync(x => x.ConcertId == reservations.Id && x.Date == dateEvent);
            var reservationss = await db.reservations.FirstOrDefaultAsync(g => g.SeanseId == seans.Id && g.UserId == UserId);
            var concert = db.concerts.FirstOrDefault(p => p.Id == reservations.Id);
            string guid = Guid.NewGuid().ToString(); 

            if(reservationss != null)
            {
                guid = reservationss.Guid;
            }

            if (orderNumbers.Count != 0)
            {
                using (var transaction = await db.Database.BeginTransactionAsync())
                {
                    for (int i = 0; i < orderNumbers.Count; i++)
                    {
                        int price = 0;
                        if (orderNumbers[i] <= 19) price = concert.PriceTicket + 400;                                               
                        if (orderNumbers[i] >= 20 && orderNumbers[i] <= 39) price = concert.PriceTicket + 300;                       
                        if (orderNumbers[i] >= 40 && orderNumbers[i] <= 59) price = concert.PriceTicket + 200;                                               
                        if (orderNumbers[i] >= 60 && orderNumbers[i] <= 79) price = concert.PriceTicket + 100;                                              
                        if (orderNumbers[i] >= 80 && orderNumbers[i] <= 99) price = concert.PriceTicket;
 
                        await db.reservations.AddAsync(new Reservation
                        {
                            SeanseId = seans.Id,
                            UserId = UserId,
                            SeatReservation = orderNumbers[i],
                            Guid = guid.ToString(),
                            Price = price,
                            Status = 0
                        });
                    }
                    await db.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var user = await db.Users.FirstOrDefaultAsync(u => u.Id == UserId);
                    
                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Afisha", ""));
                    message.To.Add(new MailboxAddress($"{user.UserName}"));
                    message.Subject = "Уведомление от Afisha!";
                    message.Body = new BodyBuilder() { HtmlBody = $"<h1  style=\"color: green;\">Good day {user.Name} {user.Surname}! On {date} a concert will take place {title}, do not forget! Your hashcode {guid}." }.ToMessageBody(); 
                                  
                    using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate("AfishaBishkek@gmail.com", "Afisha01");
                        client.Send(message);

                        client.Disconnect(true);
                    }
                    TempData["SuccessNotification"] = $" The letter has been sent to your mail. You have booked successfully!";
                    return RedirectToAction("ReservationsTicketPhilharmonics", new { date = date });
                }
            }
            TempData["ErrorMessage"] = $"You have not selected a seat for booking";
            return RedirectToAction("ReservationsTicketPhilharmonics", new { date = date } );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Afisha.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace Afisha.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger, AfishaContext _db) : base(logger, _db)
        { }

        public IActionResult Index()
        {
            var eventsView = db.seanses.Where(s => s.Date >= DateTime.Today).Include(s => s.Concert).Select(s => s.Concert).Distinct().Select(concert =>
            new EventsView
            {
                Id = concert.Id,
                Location = concert.LocationId == (int)LocationsPlace.Philharmonics ? "Philharmonics" : "Not result",
                Image = concert.Image,
                PriceTicket = concert.PriceTicket
            });

            return View(eventsView);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        public IActionResult ConcertsView()
        {
            var eventsView = db.seanses.Where(s => s.Date >= DateTime.Today).Include(s => s.Concert).Select(s => s.Concert).Distinct().Select(        
                concert =>
            new EventsView
            {
                Id = concert.Id,
                Location = concert.LocationId == (int)LocationsPlace.Philharmonics ? "Philharmonics" : "Not result",
                Image = concert.Image,
                PriceTicket = concert.PriceTicket
            });

            return View(eventsView);
        }

        public async Task<IActionResult> DetailsPage(int Id)
        {
            var seans = await db.seanses.Where(n => n.ConcertId == Id && n.Date >= DateTime.Today).ToListAsync();
            var concertDetail = from concerts in db.concerts.Where(n => n.Id == Id)
                                select
                                (
                                new DetailsPage
                                {
                                    Id = concerts.Id,
                                    TitleConcert = concerts.TitleConcert,                                  
                                    Location = concerts.LocationId == (int)LocationsPlace.Philharmonics ? "Philharmonics" : "Not result",
                                    PriceTicket = concerts.PriceTicket,
                                    HallForPerformances = concerts.HallForPerformances == HallForPerformances.BigHall ? "Big hall" : concerts.HallForPerformances == HallForPerformances.SmallHall ? "Small hall" : "Street",
                                    PhoneInfoConcert = concerts.PhoneInfoConcert,
                                    Image = concerts.Image,
                                    Description = concerts.Description,
                                    dates = seans,
                                    Duration = concerts.Duration == 0 ? "Not result" : $"{concerts.Duration}"
                                }
                                );
            return View(concertDetail.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

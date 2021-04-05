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
using Afisha.Enum;

namespace Afisha.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger, AfishaContext _db) : base(logger, _db)
        { }

        public IActionResult Index()
        {
            var eventsViews = from concert in db.concerts.Where(c => c.ConcertDate >= DateTime.Today).ToList()
                              select
                              (new EventsView
                              {
                                  Id = concert.Id,
                                  Location = concert.LocationEnumId == LocationsPlace.Philharmonics ? "Philharmonics" : "Not result",
                                  Image = concert.Image,
                                  PriceTicket = concert.PriceTicket
                              });

            return View(eventsViews);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        public IActionResult ConcertsView()
        {
            var eventsViews = from concert in db.concerts.Where(c => c.ConcertDate >= DateTime.Today).ToList()
                              select
                             (
                             new EventsView
                              {
                                  Id = concert.Id,
                                  Location = concert.LocationEnumId == LocationsPlace.Philharmonics ? "Philharmonics" : "Not result",
                                  Image = concert.Image,
                                  PriceTicket = concert.PriceTicket
                              });

            return View(eventsViews);
        }

        public async Task<IActionResult> DetailsPage(int Id)
        {
            var seans = await db.seanses.Where(n => n.ConcertId == Id && n.Date >= DateTime.Today).ToListAsync();
            var concertDetail = from app in db.concerts.Where(n => n.Id == Id)
                               select
                               (
                               new DetailsPage
                               {
                                   Id = app.Id,
                                   TitleConcert = app.TitleConcert,
                                   ConcertDate = app.ConcertDate,
                                   Location = app.LocationEnumId == LocationsPlace.Philharmonics ? "Philharmonics" : "Not result",
                                   PriceTicket = app.PriceTicket,
                                   HallForPerformances = app.HallForPerformances == HallForPerformances.BigHall ? "Big hall" : app.HallForPerformances == HallForPerformances.SmallHall ? "Small hall" : "Street",
                                   PhoneInfoConcert = app.PhoneInfoConcert,
                                   Image = app.Image,
                                   Description = app.Description,
                                   dates = seans                           
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

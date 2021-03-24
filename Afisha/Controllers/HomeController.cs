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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ConcertsView()
        {
            var AllConcerts = await db.concerts.ToListAsync();

            var viewOperator = from app in db.concerts
                               select
                               (new ConcertsView
                               {
                                   Id = app.Id,
                                   TitleConcert = app.TitleConcert,
                                   ConcertDate = app.ConcertDate,
                                   DurationOfConcertDays = app.DurationOfConcertDays,
                                   Location = app.LocationId == LocationsPlace.Philharmonics ? "Philharmonics" : "Not result",
                                   PriceTicket = app.PriceTicket,
                                   HallForPerformances = app.HallForPerformances == HallForPerformances.BigHall ? "Big hall" : app.HallForPerformances == HallForPerformances.SmallHall ? "Small hall" : "Street",
                                   PhoneInfoConcert = app.PhoneInfoConcert,
                                   Image = app.Image,
                                   Description = app.Description
                               }
                               );
            
            return View(viewOperator);
        }

        public async Task<IActionResult> Reserve(int Id)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

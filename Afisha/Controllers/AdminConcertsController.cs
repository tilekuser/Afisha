using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Afisha;
using Afisha.Models;
using Microsoft.Extensions.Logging;

namespace Afisha.Controllers
{
    public class AdminConcertsController : BaseController
    {
        public AdminConcertsController(ILogger<HomeController> logger, AfishaContext _db) : base(logger, _db)
        { }

        // GET: AdminConcerts
        public async Task<IActionResult> Index()
        {
            return View(await db.concerts.ToListAsync());
        }

        // GET: AdminConcerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await db.concerts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // GET: AdminConcerts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminConcerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TitleConcert,ConcertDate,LocationId,LocationEnumId,PriceTicket,HallForPerformances,PhoneInfoConcert,Image,Description")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                await db.concerts.AddAsync(new Concert
                {
                    Id = concert.Id,
                    LocationEnumId = concert.LocationEnumId,
                    Image = concert.Image,
                    HallForPerformances = concert.HallForPerformances,
                    PriceTicket = concert.PriceTicket,
                    TitleConcert = concert.TitleConcert,
                    PhoneInfoConcert = concert.PhoneInfoConcert,
                    ConcertDate = concert.ConcertDate,
                    Description = concert.Description,
                    LocationId = concert.LocationEnumId == LocationsPlace.Philharmonics ? 1 : 0
                });
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concert);
        }

        // GET: AdminConcerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await db.concerts.FindAsync(id);
            if (concert == null)
            {
                return NotFound();
            }
            return View(concert);
        }

        // POST: AdminConcerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TitleConcert,LocationId,ConcertDate,LocationEnumId,PriceTicket,HallForPerformances,PhoneInfoConcert,Image,Description")] Concert concertEdit)
        {
            if (id != concertEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    //concert.Id = concertEdit.Id;
                    //concert.LocationEnumId = concertEdit.LocationEnumId;
                    //concert.Image = concertEdit.Image;
                    //concert.HallForPerformances = concertEdit.HallForPerformances;
                    //concert.PriceTicket = concertEdit.PriceTicket;
                    //concert.TitleConcert = concertEdit.TitleConcert;
                    //concert.PhoneInfoConcert = concertEdit.PhoneInfoConcert;
                    //concert.ConcertDate = concertEdit.ConcertDate;
                    //concert.Description = concertEdit.Description;
                    db.Update(concertEdit);
                    var concert = await db.concerts.FirstOrDefaultAsync(l => l.Id == concertEdit.Id);
                    concert.LocationId = concert.LocationEnumId == LocationsPlace.Philharmonics ? 1 : 0;

                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertExists(concertEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(concertEdit);
        }

        // GET: AdminConcerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await db.concerts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // POST: AdminConcerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concert = await db.concerts.FindAsync(id);
            db.concerts.Remove(concert);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertExists(int id)
        {
            return db.concerts.Any(e => e.Id == id);
        }
    }
}

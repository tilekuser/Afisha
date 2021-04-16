using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afisha.Models
{
    public class ReservationsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BeginPriceTicket { get; set; }
        public DateTime Date { get; set; }
        public List<int> Seatreservations { get; set; }
        public int TotalSeats { get; set; }
        public string UserName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afisha.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int SeanseId { get; set; }
        public int UserId { get; set; }
        public int SeatReservation { get; set; }
    }
}

using Afisha.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afisha.Models
{
    public class Concert
    {
        public int Id { get; set; }
        public string TitleConcert { get; set; }
        public DateTime ConcertDate { get; set; }
        public int DurationOfConcertDays { get; set; }
        public LocationsPlace LocationId { get; set; }
        public int PriceTicket { get; set; }
        public HallForPerformances HallForPerformances { get; set; }
        public string PhoneInfoConcert { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

    }
}

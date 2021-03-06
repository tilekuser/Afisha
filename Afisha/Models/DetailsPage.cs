using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afisha.Models
{
    public class DetailsPage
    {
        public int Id { get; set; }
        public string TitleConcert { get; set; }
        public DateTime ConcertDate { get; set; }
        public string Location { get; set; }
        public int PriceTicket { get; set; }
        public string HallForPerformances { get; set; }
        public string PhoneInfoConcert { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public List<Seans> dates { get; set; }
        public string Duration { get; set; }
    }
}

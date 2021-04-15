
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Afisha.Models
{
    public class AdminConcertCRUD
    {
        public int Id { get; set; }
        [Required]
        public string TitleConcert { get; set; }
        public int LocationId { get; set; }
        [Required]
        public LocationsPlace LocationEnumId { get; set; }
        [Required]
        public int PriceTicket { get; set; }
        [Required]
        public HallForPerformances HallForPerformances { get; set; }
        public string PhoneInfoConcert { get; set; }
        [Required]
        public string Image { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime[] dateTimes { get; set; }
        [Required]
        public int Duration { get; set; }

    }
}

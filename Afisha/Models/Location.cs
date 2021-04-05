using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afisha.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string PhoneInformation { get; set; }
        public string WorkTime { get; set; }
        public string Image { get; set; }
        public int TotalSeats { get; set; }

    }
}

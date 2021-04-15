using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afisha.Models
{
    public class Seans
    {
        public int Id { get; set; }
        public int ConcertId { get; set; }
        public DateTime Date { get; set; }
        public Concert Concert { get; set; }

    }
}

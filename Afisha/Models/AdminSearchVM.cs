using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afisha.Models
{
    public class AdminSearchVM
    {
        public string UserName { get; set; }
        public string TitleEvent { get; set; }
        public List<int> Seats { get; set; }
        public DateTime Date { get; set; }
        public int TotalPrice { get; set; }
        public int Status { get; set; }
        public string Guid { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheapAwesome.Models
{
    public class HotelRates
    {
        public Hotel hotel { get; set; }
        public List<Rates> rates { get; set; }
    }
}

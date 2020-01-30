using CheapAwesome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheapAwesome.Services
{
    public interface IBargainService
    {
        public Task<List<HotelRates>> FindBargain(int night,int destinationId);
    }
}

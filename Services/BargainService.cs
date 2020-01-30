using CheapAwesome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CheapAwesome.Services
{
    public class BargainService : IBargainService
    {
        
        public async Task<List<HotelRates>> FindBargain(int night, int destinationId)
        {
            try
                
            {
                
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://webbedsdevtest.azurewebsites.net/api/findBargain");
                    var streamTask = await client.GetStreamAsync($"?code=aWH1EX7ladA8C/oWJX5nVLoEa4XKz2a64yaWVvzioNYcEo8Le8caJw==&destinationId={destinationId}&nights={night}");

                    var result = await JsonSerializer.DeserializeAsync<List<HotelRates>>(streamTask);
                     
                    Parallel.ForEach(result.Where(x => x.rates.Any(x => x.rateType == "PerNight")).Select(x => x.rates).ToList(), item =>
                    {
                        Parallel.ForEach(item, rate => { rate.value = rate.value * night; });
                    });


                    return result;
                }
                
                
               
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}

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
        //used HTTPclient without factory increase the execution time to 6 sc
        private readonly HttpClient _client;

        public BargainService(HttpClient client)
        {
            this._client = client;
        }
      
        private  HttpRequestMessage CreateRequest(int nights, int destinationId)
        {
            var path = _client.BaseAddress.AbsoluteUri +$"&destinationId={destinationId}&nights={nights}";
            return new HttpRequestMessage(HttpMethod.Get, new Uri(path));
        }
        public async Task<List<HotelRates>> FindBargain(int nights, int destinationId)
        {
            try
            {
                var request = CreateRequest(nights,destinationId);
             
                var result = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                using (var contentStream = await result.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<HotelRates>>(contentStream);

                    Parallel.ForEach(data.Where(x => x.rates.Any(x => x.rateType == "PerNight")).Select(x => x.rates).ToList(), item =>
                    {
                        Parallel.ForEach(item, rate => { rate.value = rate.value * nights; });
                    });
                    return data;
                }
                 

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheapAwesome.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CheapAwesome.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BargainController : ControllerBase
    {

        private readonly IBargainService _bargainService;
        public BargainController(IBargainService bargainService)
        {
            _bargainService = bargainService;

        }
        public async Task<IActionResult> Index(int night,int destinationId)
        {
            var result = await _bargainService.FindBargain(night, destinationId);

            return Ok(result);
        }
       
    }
}

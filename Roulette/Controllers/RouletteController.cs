using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using Casino.Models;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Linq;

namespace Casino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : Controller
    {
        private IConfiguration configuration;

        public RouletteController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        [Route("Create")]
        public string Create() => new RouletteBL(configuration["DBServer"]).Create();

        [HttpPost]
        [Route("Open")]
        public string Open([FromBody]string id) => new RouletteBL(configuration["DBServer"]).Open(id);

        [HttpPost]
        [Route("Bet")]
        public string Bet([FromBody]RouletteModel roulette)
        {
            roulette.Bet.Bettor = Request.Headers["UserId"];

            return new RouletteBL(configuration["DBServer"]).Bet(bet: roulette.Bet, maxAmount: Double.Parse(configuration["BetMaxAmount"]), 
                id: roulette.Id, allowedColors: configuration["ColorsForBet"].Split(","), rangeNumbersBet: configuration["RangeNumberForBet"].Split(","));
        }

        [HttpPost]
        [Route("Closed")]
        public List<Bet> Closed([FromBody] string id) => new RouletteBL(configuration["DBServer"]).Closed(id);

        [HttpGet]
        [Route("GetRoulettes")]
        public List<Roulette> GetRoulettes() => new RouletteBL(configuration["DBServer"]).GetRoulettes();
    }
}

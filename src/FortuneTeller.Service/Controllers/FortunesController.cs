using FortuneTeller.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FortuneTeller.Service.Controllers
{ 

    [Route("api/[controller]")]
    [ApiController]
    public class FortunesController : ControllerBase
    {
        private readonly IFortuneRepository _fortuneRepository;
        private readonly ILogger<FortunesController> _logger;

        public FortunesController(ILogger<FortunesController> logger, IFortuneRepository fortuneRepository)
        {
            _logger = logger;
            _fortuneRepository = fortuneRepository;
        }

        // GET: api/fortunes/all
        [HttpGet("all")]
        public async Task<List<Fortune>> AllFortunesAsync()
        {
            _logger?.LogTrace("AllFortunesAsync");
            var fortunesLi = await _fortuneRepository.GetAllAsync();
            return fortunesLi.Select(f => new Fortune() { Id = f.Id, Text = f.Text }).ToList();

          //  return await Task.FromResult(new List<Fortune>() { new Fortune() { Id = 1, Text = "Hello from FortuneController Web API!" } });
        }

        // GET api/fortunes/random
        [HttpGet("random")]
        public async Task<Fortune> RandomFortuneAsync()
        {
            _logger?.LogTrace("RandomFortuneAsync");
            var fortune = await _fortuneRepository.RandomFortuneAsync();
            return new Fortune() { Id = fortune.Id, Text = fortune.Text };
        }
    }
}

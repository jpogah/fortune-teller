using System;
using System.Threading.Tasks;
using Steeltoe.CircuitBreaker.Hystrix;

namespace FortuneTeller.UI.Services
{
    public class FortuneServiceCommand : HystrixCommand<Fortune>
    {
        private readonly IFortuneService _fortuneService;
        public FortuneServiceCommand(IHystrixCommandOptions options,IFortuneService fortuneService) : base(options)
        {
            _fortuneService = fortuneService;
            IsFallbackUserDefined = true;
        }

        public async Task<Fortune> RandomFortuneAsync()
        {
            return await ExecuteAsync();

        }

        protected override async Task<Fortune> RunAsync()
        {
            return await _fortuneService.RandomFortuneAsync();
        }

        protected override async Task<Fortune> RunFallbackAsync()
        {
            return await Task.FromResult<Fortune>(new Fortune() { Id = 9999, Text = "You will have a happy day!" });
        }
    }
}

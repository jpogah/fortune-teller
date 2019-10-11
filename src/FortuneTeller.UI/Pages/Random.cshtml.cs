using FortuneTeller.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace FortuneTeller.UI.Pages
{
    public class RandomModel : PageModel
    {
        public string Message { get; private set; } = "Hello from FortuneTellerUI!";
        public IFortuneService _fortuneService;
        public RandomModel(IFortuneService fortuneService)
        {
            _fortuneService = fortuneService;
        }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var fortune = await _fortuneService.RandomFortuneAsync();
            Message = fortune.Text;
            HttpContext.Session.Set("MyFortune", Encoding.ASCII.GetBytes(Message));
        }
    }
}
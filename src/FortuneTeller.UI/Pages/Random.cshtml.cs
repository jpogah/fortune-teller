using FortuneTeller.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace FortuneTeller.UI.Pages
{
    public class RandomModel : PageModel
    {
        public string Message { get; private set; } = "Hello from FortuneTellerUI!";
        public FortuneServiceCommand _fortuneServiceCommand;
        public RandomModel(FortuneServiceCommand fortuneServiceCommand)
        {
            _fortuneServiceCommand = fortuneServiceCommand;
        }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var fortune = await _fortuneServiceCommand.RandomFortuneAsync();
            Message = fortune.Text;
            HttpContext.Session.Set("MyFortune", Encoding.ASCII.GetBytes(Message));
        }
    }
}
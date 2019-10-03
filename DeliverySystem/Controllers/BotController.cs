using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace DeliverySystem.Controllers
{
    [ApiController]
    public class BotController : ControllerBase
    {
        [Route(@"api/bot/update")]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            var commands = Bot.Bot.Commands;
            var message = update.Message;
            var client = await Bot.Bot.Get();

            foreach (var command in commands)
            {
                if (command.Contains(message.Text))
                {
                    command.Execute(message, client);
                    break;
                }
            }
            return Ok();
        }
    }
}
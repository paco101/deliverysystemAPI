using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DeliverySystem.Controllers
{
    [ApiController]
    public class BotController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public BotController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route(@"api/bot/update")]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            var message = update.Message;
            var courier = _dbContext.Couriers.FirstOrDefault(c => c.TelegramUsrName == message.From.Username);
            var client = await Bot.Bot.GetClient();
            if (courier == null)
            {
                await client.SendTextMessageAsync(message.Chat.Id,
                    "Sorry u're not registed. More info at https://github.com/RNRNRNR/deliverysystemAPI");
                return Ok();
            }

            var commands = Bot.Bot.Commands;

            foreach (var command in commands)
            {
                if (message.Text.Contains(command.Name))
                {
                    command.Execute(message, client);
                    break;
                }
            }
            
            return Ok();
        }
    }
}
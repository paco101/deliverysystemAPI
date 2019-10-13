using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Bot.Commands;
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
        public async Task<OkResult> Update([FromBody] Update update) // Handling bot updates
        {
            var message = update.Message;
            var courier = _dbContext.Couriers.FirstOrDefault(c => c.TelegramUsrName == message.From.Username);
            var client = Bot.Bot.GetClient();
            if (courier == null) // Check registration(is he courier)
            {
                await client.SendTextMessageAsync(message.Chat.Id,
                    "Sorry u're not registed. More info at https://github.com/RNRNRNR/deliverysystemAPI");
                return Ok();
            }

            switch (courier.Status)
            {
                case 3:
                    var confirm = new ConfirmCommand();
                    if (message.Text.Contains(confirm.Name))
                        confirm.Execute(message, client, _dbContext);
                    else
                        await client.SendTextMessageAsync(message.Chat.Id, "To confirm delivery type /confirmdelivery");

                    break;
                case 1:
                    var work = new WorkCommand();
                    if (message.Text.Contains(work.Name))
                        work.Execute(message, client, _dbContext);
                    break;
                case 2:
                    await client.SendTextMessageAsync(message.Chat.Id, "Receive ur packages");
                    break;
                case null:
                    var start = new StartCommand();
                    if (message.Text.Contains(start.Name))
                        start.Execute(message, client, _dbContext);
                    break;
            }

            return Ok();
        }
    }
}
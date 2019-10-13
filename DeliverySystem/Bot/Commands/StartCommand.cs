using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DeliverySystem.Bot.Commands
{
    public class StartCommand : Command // Start Up command
    {
        public override string Name => "/start";

        public override async void Execute(Message message, TelegramBotClient client,ApiDbContext dbContext=null)
        {
            var courier = dbContext.Couriers.FirstOrDefault(c => c.TelegramUsrName == message.From.Username);
            courier.Status = 1;
            courier.TelegramChatId = message.Chat.Id;
            await dbContext.SaveChangesAsync();
            
            var  keyboard = new ReplyKeyboardMarkup(new KeyboardButton[]
            {new KeyboardButton("/work"),
            }, oneTimeKeyboard: true);
            
            var result = client.SendTextMessageAsync(message.Chat.Id, "U're registed, and ready to /work",
                replyMarkup: keyboard);
        }
    }
}
using System.Collections.Generic;
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
    public class StartCommand : Command
    {
        public override string Name => "/start";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var  keyboard = new ReplyKeyboardMarkup(new KeyboardButton[]
            {new KeyboardButton("/work"),
            }, oneTimeKeyboard: true);
            
            var result = client.SendTextMessageAsync(message.Chat.Id, "U're registed, and ready to /work",
                replyMarkup: keyboard);
        }
    }
}
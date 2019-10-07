using System;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DeliverySystem.Bot.Commands
{
    public class WorkCommand : Command
    {
        public override string Name => "/work";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var startWorkTime = Config.AppConfiguration.StartWorkTime;
            var endWorkTime = Config.AppConfiguration.EndWorkTime;


            // Check work time
            if (DateTime.Now.TimeOfDay > Config.AppConfiguration.EndWorkTime ||
                DateTime.Now.TimeOfDay < Config.AppConfiguration.StartWorkTime)
            {
                await client.SendTextMessageAsync(message.Chat.Id,
                    $"Sorry, u're not in time. U'r work time is {startWorkTime:HH-mm}- {endWorkTime:HH-mm}. Come back in time ");
                return;
            }

            await client.SendTextMessageAsync(message.Chat.Id, "Are u sure?",
                replyMarkup: new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Yes", "WorkYesCallback"),
                        InlineKeyboardButton.WithCallbackData("No", "DefaultNoCallback"),
                    }
                }));
            //We calling main method by event
        }

        public async Task StartWork(ApiDbContext dbContext, TelegramBotClient client,
                Message message) // Main method which outputs user, list of deliveries
        {
            await client.SendTextMessageAsync(message.Chat.Id, "Wait few second, i'm choosing the orders for u");

            var courier = dbContext.Couriers.FirstOrDefaultAsync(c => c.TelegramUsrName == message.From.Username)
                .Result;
            var deliveries = dbContext.DeliveryOrders.Where(c => !c.IsDelivered);
            for (int i = 0; i < dbContext.Stocks.Count(); i++)
            {
            }
        }
    }
}
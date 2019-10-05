using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeliverySystem.Bot.Commands
{
    public class WorkCommand : Command
    {
        public override string Name => "/work";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var startWorkTime = Config.AppConfiguration.StartWorkTime;
            var endWorkTime = Config.AppConfiguration.EndWorkTime;
            if (DateTime.Now.TimeOfDay > Config.AppConfiguration.EndWorkTime ||
                DateTime.Now.TimeOfDay < Config.AppConfiguration.StartWorkTime)
            {
                await client.SendTextMessageAsync(message.Chat.Id,
                    $"Sorry, u're not in time. U'r work time is {startWorkTime}- {endWorkTime}. Come back in time ");
                return;
            }
            
        }
    }
}
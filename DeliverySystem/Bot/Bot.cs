using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Bot.Commands;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace DeliverySystem.Bot
{
    public static class Bot
    {
        private static TelegramBotClient _client;
        

        public static async Task<TelegramBotClient> Initialize(ApiDbContext dbContext=null)
        {
            _client = new TelegramBotClient(Config.AppConfiguration.ApiKey);
            var hook = string.Format(Config.AppConfiguration.Url, "api/bot/update");
            await _client.SetWebhookAsync(hook);

            _client.OnCallbackQuery += async (sender, args) =>
            {
                switch (args.CallbackQuery.Data)
                {
                    case "WorkYesCallback":

                        var work = new WorkCommand();
                        await work.StartWork(_client, args.CallbackQuery, dbContext);
                        break;
                    case "DefaultNoCallback":
                        await _client.SendTextMessageAsync(args.CallbackQuery.Message.Chat.Id, "Ok,lol");
                        break;
                }
            };
            return _client;
        }

        public static  TelegramBotClient GetClient()
        {
            return _client;
        }
    }
}
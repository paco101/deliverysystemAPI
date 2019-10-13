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
        
        public static async Task<TelegramBotClient> Initialize()
        {
            _client = new TelegramBotClient(Config.AppConfiguration.ApiKey);
            var hook = string.Format(Config.AppConfiguration.Url, "api/bot/update");
            await _client.SetWebhookAsync(hook);
            return _client;
        }

        public static  TelegramBotClient GetClient()
        {
            return _client;
        }
    }
}
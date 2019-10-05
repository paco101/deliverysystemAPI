using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.Bot.Commands;
using Telegram.Bot;

namespace DeliverySystem.Bot
{
    public static class Bot
    {
        private static TelegramBotClient _client;
        private static List<Command> _commandsList;

        public static IReadOnlyList<Command> Commands => _commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> Initialize()
        {
            _commandsList = new List<Command> {new StartCommand(),new WorkCommand() };

            _client = new TelegramBotClient(Config.AppConfiguration.ApiKey);
            var hook = string.Format(Config.AppConfiguration.Url, "api/bot/update");
            await _client.SetWebhookAsync(hook);
            return _client;
        }

        public static async Task<TelegramBotClient> GetClient()
        {
            if (_client != null)
                return _client;
            await Initialize();
            return _client;
        }
    }
}
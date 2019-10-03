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

        public static async Task<TelegramBotClient> Get()
        {
            if (_client != null)
                return _client;

            _commandsList = new List<Command> {};

            _client= new TelegramBotClient(Config.AppConfiguration.ApiKey);
            var hook = string.Format(Config.AppConfiguration.Url, "api/bot/update");
            await _client.SetWebhookAsync(hook);

            return _client;
        }
        
    }
}
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeliverySystem.Bot.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract void Execute(Message message, TelegramBotClient client);
        
    }
}
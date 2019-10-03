using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeliverySystem.Bot.Commands
{
    public class TestCommand : Command
    {
        public override string Name => "test";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await client.SendTextMessageAsync(chatId, "Hello!", replyToMessageId: messageId);
        }
    }
}
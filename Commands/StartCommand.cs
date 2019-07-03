using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace calendar_flood_bot.Commands
{
    public class StartCommand : Command
    {
        public override string Name => @"/start";

        public override bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            return message.Text.Contains(this.Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient)
        {
            
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, "Привет, я календарный бод.\n Отправь /calendar и найду какие сегодня праздники", parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
        }
    }
}

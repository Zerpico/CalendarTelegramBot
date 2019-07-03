using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System;
using System.Globalization;

namespace calendar_flood_bot.Commands
{
    public class CalendarCommand : Command
    {
        public override string Name => "/calendar";

        public override bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            return message.Text.Contains(this.Name);

        }


        public override async Task Execute(Message message, TelegramBotClient botClient)
        {
            try
            {                
                CultureInfo locale = new CultureInfo("ru-RU");
                DateTime now = DateTime.Now;

                var celebration = await Services.Celebration.GetCelebrationToday();
                var chatId = message.Chat.Id;
                await botClient.SendTextMessageAsync(chatId, "🎉 Сегодня 🎉  " + now.ToString("D", locale) + " - "+ now.ToString("ddd", locale) + "\n\n"+celebration, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
            }
            catch (System.Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, ex.Message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            }
        }
    }
    
    
}

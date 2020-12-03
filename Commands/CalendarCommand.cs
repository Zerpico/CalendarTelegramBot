using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System;
using System.Globalization;
using System.Linq;

namespace CalendarTelegramBot.Commands
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
                //init vars
                CultureInfo locale = new CultureInfo("ru-RU");
                
                //Преобразует местное время во время в формате UTC и МСК пояс
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
                var nowUtc = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cstZone);


                string result = string.Empty;

                //получаем список праздников
                var celebration = await Services.Celebration.GetCelebrationToday();
                //немного форматируем праздники
                for (int i = 0; i < celebration.Count(); i++)
                {
                    result += "• " + celebration[i] + System.Environment.NewLine;
                }

                var chatId = message.Chat.Id;
                await botClient.SendTextMessageAsync(chatId, "🎉 Сегодня 🎉  " + nowUtc.ToString("D", locale) + "\n\n" + celebration, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
            }
            catch (System.Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, ex.Message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Default);
            }
        }
    }

}

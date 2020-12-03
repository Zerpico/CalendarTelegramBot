using System.Threading.Tasks;
using Telegram.Bot;
using System;
using System.Globalization;
using System.Linq;

namespace CalendarTelegramBot.Commands
{
    public class CalendarCommand : BaseCommand
    {
        public override string Name => "/calendar";

        public CalendarCommand(ITelegramBotClient bot)
            : base(bot)
        {

        }

        public override async Task ExecuteAsync(CommandEventArgs e)
        {
            //init vars
            CultureInfo locale = new CultureInfo("ru-RU");

            TimeZoneInfo cstZone = null;
            //Преобразует местное время во время в формате UTC и МСК пояс
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                cstZone = TimeZoneInfo.FindSystemTimeZoneById(@"Russian Standard Time");
            else
                cstZone = TimeZoneInfo.FindSystemTimeZoneById(@"Europe/Moscow");

            var nowUtc = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cstZone);


            string result = string.Empty;

            //получаем список праздников
            var celebration = await Services.Celebration.GetCelebrationToday();
            //немного форматируем праздники
            for (int i = 0; i < celebration.Count(); i++)
            {
                result += "• " + celebration[i] + System.Environment.NewLine;
            }

           

            await Bot.SendTextMessageAsync(e.ChatId, "🎉 Сегодня 🎉  " + nowUtc.ToString("D", locale) + "\n\n" + result, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);

           /* await Bot.SendTextMessageAsync(e.ChatId,
                "Это календарный бот.\n" +
                "Отправь /calendar и найду какие сегодня праздники");*/
        }
    }
    
}

using CalendarTelegramBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Linq;
using System;
using System.Collections.Generic;

namespace CalendarTelegramBot.Commands
{
    public class DefaultCommand : BaseCommand
    {
        public override string Name => string.Empty;
        Random rnd = new Random();

        public DefaultCommand(ITelegramBotClient bot)
            : base(bot)
        {

        }

        public override async Task ExecuteAsync(CommandEventArgs e)
        {
            
            var text = e.CommandLine.ToLower();
            var findText = ParseText(text);

            if (findText == string.Empty)
                return;

            using (var dbContext = new Context.DatabaseContext())
            {
                DateTime now = DateTime.Now;
                DateTime startDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

                var countOnion = dbContext.DbLogs.Where(d => d.ChatId == e.ChatId && d.UserId == e.UserId && (d.Dt >= startDay && d.Dt < startDay.AddDays(1))).Count();

                if (!e.IsBotOwner)
                if (countOnion <= 10 )
                {
                    dbContext.DbLogs.Add(
                        new Context.DbLog()
                        {
                            ActionType = (int)Context.ActionType.onion,
                            ChatId = e.ChatId,
                            UserId = e.UserId,
                            Dt = DateTime.Now
                        });
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    await Bot.SendTextMessageAsync(e.ChatId, "Вы исчерпали луковый поиск на сегодня", replyToMessageId: (int)e.MessageId, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                    return;
                }
            }
          

            var imgUrls = await OnionSearch.SearchOnion2(findText);


            var url = imgUrls[rnd.Next(0, imgUrls.Count < 10 ? imgUrls.Count : 10)];

            await Bot.SendPhotoAsync(e.ChatId, new Telegram.Bot.Types.InputFiles.InputOnlineFile(url), "Кто-то сказал лук?" + Environment.NewLine + "<code> &gt;" + findText + "</code>", replyToMessageId: (int)e.MessageId, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
            
        }

        static string ParseText(string text)
        {
            string result = string.Empty;
            if (text.Contains("лук") || text.Contains("лучок"))
            {
                string[] arr = text.ToString().Split(
                   new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int find = -1;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Contains("лук") || arr[i].Contains("лучок"))
                    {
                        find = i;
                        result = arr[i];
                        break;
                    }
                }

                if (find < arr.Length - 1) result += " " + arr[find + 1];
                if (find > 0 && arr.Length > 1) result = arr[find - 1] + " " + result;               
            }
            return result;
        }
    }
}

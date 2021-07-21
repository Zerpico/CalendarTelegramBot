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

            var imgUrls = OnionSearch.SearchOnion2(findText);


            var url = imgUrls[rnd.Next(0, imgUrls.Count < 10 ? imgUrls.Count : 10)];


            await Bot.SendTextMessageAsync(e.ChatId, "<i>Кто-то сказал <a href=\"" + url + "\">лук</a>?</i>" + Environment.NewLine+ "<code> &gt;" + findText + "</code>"+Environment.NewLine , replyToMessageId: (int)e.MessageId, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);


            //await Bot.SendTextMessageAsync(e.ChatId, "Я не знаю такую команду.", replyToMessageId: (int)e.MessageId);
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

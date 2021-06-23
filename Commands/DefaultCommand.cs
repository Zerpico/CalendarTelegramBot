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
            IEnumerable<PexelsDotNetSDK.Models.Photo> finds = null;
            
            var text = e.CommandLine.ToLower();
            if (text.Contains("лук"))
            {
                if (text.Contains("собака") || text.Contains("сабака"))
                    finds = await OnionSearch.SearchOnion("dog+onion");
                else finds = await OnionSearch.SearchOnion("onion");                
            }
            else if (text.Contains("лучок"))
            {
                finds = await OnionSearch.SearchOnion("onion");
            }    

            if (finds == null)
                return;

            int number = 1;            
            var tagfinds = finds.ToDictionary(x => number++, x => x.source.portrait);
            var url = tagfinds[rnd.Next(1, tagfinds.Count)];

         
            string[] splits = e.CommandLine.Split(new string[]{ " ", ".", "/r/n", "/n", Environment.NewLine }, StringSplitOptions.None);
            var reply = splits.Where(d => d.ToLower().Contains("лук") || d.ToLower().Contains("лучок")).FirstOrDefault();


            await Bot.SendTextMessageAsync(e.ChatId, "<i>Кто-то сказал <a href=\"" + url + "\">лук</a>?</i>" + Environment.NewLine+ "<code> &gt;" + reply + "</code>"+Environment.NewLine , replyToMessageId: (int)e.MessageId, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);


            //await Bot.SendTextMessageAsync(e.ChatId, "Я не знаю такую команду.", replyToMessageId: (int)e.MessageId);
        }
    }
}

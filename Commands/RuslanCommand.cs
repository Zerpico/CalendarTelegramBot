using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;


namespace CalendarTelegramBot.Commands
{
    public class RuslanCommand : BaseCommand
    {
        public override string Name => "/ruslan";

        public RuslanCommand(ITelegramBotClient bot)
            : base(bot)
        {

        }

        public override async Task ExecuteAsync(CommandEventArgs e)
        {
            if (e.IsBotOwner || e.ChatId == -1001482203145)
            {

                //  if (!e.IsBotOwner)
                //      return;


                Random rnd = new Random();


                var files = System.IO.Directory.GetFiles(Environment.GetEnvironmentVariable("RUSLAN"));
                var file = files[rnd.Next(0, files.Length - 1)];

                var audio = System.IO.File.ReadAllBytes(file);

                await Bot.SendVoiceAsync(e.ChatId, new Telegram.Bot.Types.InputFiles.InputOnlineFile(new System.IO.MemoryStream(audio)), replyToMessageId: (int)e.MessageId);
            }
        }
    }
    
}

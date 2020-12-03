using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace CalendarTelegramBot.Commands
{
    public class SayCommand : BaseCommand
    {
        public override string Name => "/say";

        public SayCommand(ITelegramBotClient bot)
            : base(bot)
        {

        }

        public override async Task ExecuteAsync(CommandEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.CommandLine))
                return;

         //   if (!e.IsBotOwner)
         //   {
         //       await Bot.SendTextMessageAsync(e.ChatId, "Вам запрешена эта команда", replyToMessageId: (int)e.MessageId);
         //       return;
         //   }

            var me = await Bot.GetMeAsync();

            var audio = await Services.Speach.Synthes(e.CommandLine);
            if (audio == null)
                return;

            var name = e.CommandLine.Length > 14 ? e.CommandLine.Substring(0, 14) : e.CommandLine;


            await Bot.SendVoiceAsync(e.ChatId, new Telegram.Bot.Types.InputFiles.InputOnlineFile(new System.IO.MemoryStream(audio), name), replyToMessageId: (int)e.MessageId);

        }
    }

}

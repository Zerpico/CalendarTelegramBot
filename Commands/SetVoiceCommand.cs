using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace CalendarTelegramBot.Commands
{
    public class SetVoice : BaseCommand
    {
        public override string Name => "/setvoice";

        public SetVoice(ITelegramBotClient bot)
            : base(bot)
        {

        }

        public override async Task ExecuteAsync(CommandEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.CommandLine))
                return;

            if (!e.IsBotOwner)
            {
                return;
            }



            var voice = e.CommandLine.Replace("_n", "");

            switch (voice)
            {
                case "Dasha":
                    Services.Speach._speaker = Services.speaker.Dasha_n;
                    await Bot.SendTextMessageAsync(e.ChatId, "Голос Dasha установлен", replyToMessageId: (int)e.MessageId);
                    break;
                case "Anna":
                    Services.Speach._speaker = Services.speaker.Anna_n;
                    await Bot.SendTextMessageAsync(e.ChatId, "Голос Anna установлен", replyToMessageId: (int)e.MessageId);
                    break;
                case "Julia":
                    Services.Speach._speaker = Services.speaker.Julia_n;
                    await Bot.SendTextMessageAsync(e.ChatId, "Голос Julia установлен", replyToMessageId: (int)e.MessageId);
                    break;
                case "Vladimir":
                    Services.Speach._speaker = Services.speaker.Vladimir_n;
                    await Bot.SendTextMessageAsync(e.ChatId, "Голос господина установлен", replyToMessageId: (int)e.MessageId);
                    break;
            }

        }
    }

    
    
}

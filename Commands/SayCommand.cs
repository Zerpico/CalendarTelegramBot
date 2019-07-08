using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace calendar_flood_bot.Commands
{
    public class SayCommand : Command
    {        
        public override string Name => "/say ";

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
                if (message.Text.Length > 500)
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Я не могу обработать такие длинные предложения!", replyToMessageId: message.MessageId, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                else
                {
                    var name = message.Text.Remove(0, 5).Length > 14 ? message.Text.Remove(0, 5).Substring(0, 14) : message.Text.Remove(0, 5);
                    Services.Speech speech = new Services.Speech();
                    byte[] file = await speech.Synthes(message.Text.Remove(0, 5), Services.format.mp3, Services.speaker.omazh, Services.quality.lo, 0.9, Services.emotion.evil);
                    await botClient.SendAudioAsync(message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(new System.IO.MemoryStream(file), name), replyToMessageId: message.MessageId);
                }
            }
            catch(System.Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, ex.Message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
            }
        }
    }
}

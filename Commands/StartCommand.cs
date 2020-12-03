using System.Threading.Tasks;
using Telegram.Bot;

namespace CalendarTelegramBot.Commands
{
    /// <summary>
    /// "/start" command handler
    /// </summary>
    public class StartCommand : BaseCommand
    {
        public override string Name => "/start";

        public StartCommand(ITelegramBotClient bot)
            : base(bot)
        {

        }

        public override async Task ExecuteAsync(CommandEventArgs e)
        {
            await Bot.SendTextMessageAsync(e.ChatId,
                "Это календарный бот.\n"+
                "Отправь /calendar и найду какие сегодня праздники");
        }
    }
}

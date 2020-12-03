using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CalendarTelegramBot.Handlers
{
    /// <summary>
    /// Message handler interface
    /// </summary>
    public interface IMessageHandler
    {
        ITelegramBotClient Bot { get; }

        /// <summary>
        /// Handle incoming message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task HandleAsync(Message message);
    }
}

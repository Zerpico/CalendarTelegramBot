using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CalendarTelegramBot.Handlers
{
    /// <summary>
    /// Inline query handler interface
    /// </summary>
    public interface IQueryHandler
    {
        ITelegramBotClient Bot { get; }

        /// <summary>
        /// Handle inline query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task HandleAsync(InlineQuery query);
    }
}

using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CalendarTelegramBot.Handlers
{
    /// <summary>
    /// Inline query handler base class
    /// </summary>
    public abstract class BaseQueryHandler : IQueryHandler
    {
        public ITelegramBotClient Bot { get; private set; }

        protected BaseQueryHandler(ITelegramBotClient bot)
        {
            Bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }

        /// <inheritdoc />
        public abstract Task HandleAsync(InlineQuery query);
    }
}

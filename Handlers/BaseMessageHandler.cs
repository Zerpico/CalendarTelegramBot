using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CalendarTelegramBot.Handlers
{
    /// <summary>
    /// Message handler base class
    /// </summary>
    public abstract class BaseMessageHandler : IMessageHandler
    {
        public ITelegramBotClient Bot { get; private set; }

        protected BaseMessageHandler(ITelegramBotClient bot)
        {
            Bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }

        /// <inheritdoc />
        public abstract Task HandleAsync(Message message);
    }
}

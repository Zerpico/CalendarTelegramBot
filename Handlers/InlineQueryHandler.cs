using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

namespace CalendarTelegramBot.Handlers
{
    /// <summary>
    /// Inline query handler
    /// </summary>
    public class InlineQueryHandler : BaseQueryHandler
    {
        private readonly ILogger _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bot">Reference to bot</param>
        /// <param name="defaultCommand">If no commands matched, then default command will be executed</param>
        /// <param name="commands">All possible commands list</param>
        public InlineQueryHandler(
            ILogger logger,
            ITelegramBotClient bot)
            : base(bot)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            _log = logger.ForContext<TextMessageHandler>();
        }

        /// <inheritdoc />
        public override async Task HandleAsync(InlineQuery query)
        {
            _log.Information("Handling a new inline query: {query}", query.Query);

            if (string.IsNullOrWhiteSpace(query.Query))
                return;

            var me = await Bot.GetMeAsync();


            await Bot.AnswerInlineQueryAsync(query.Id, Enumerable.Empty<InlineQueryResultBase>());
           
        }
    }
}

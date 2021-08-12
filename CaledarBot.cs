using CalendarTelegramBot.Commands;
using CalendarTelegramBot.Handlers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace CalendarTelegramBot
{
    public class CaledarBot
    {
        private readonly ILogger _log;
        private readonly ITelegramBotClient _bot;
        private readonly IDictionary<MessageType, IMessageHandler> _messageHandlers;
        private IQueryHandler _queryHandler;

        public CaledarBot(ILogger logger, ITelegramBotClient bot)
        {
            _log = logger.ForContext<CaledarBot>();
            _bot = bot;
            _messageHandlers = new Dictionary<MessageType, IMessageHandler>();

            InitEvents();
            InitHandlers();
        }

        private void InitEvents()
        {
            _log.Information("Registering event handlers...");

            _bot.OnMessage += async (s, e) => await OnMessage(s, e);
            _bot.OnInlineQuery += async (s, e) => await OnInlineQuery(s, e);
        }

        private void InitHandlers()
        {
            _log.Information("Registering message handlers...");
                       

            var commands = new ICommand[]
            {
              //  new HelpCommand(_bot),
                new StartCommand(_bot),
                new CalendarCommand(_bot),
                new SayCommand(_bot),
                new SetVoice(_bot),
                 new RuslanCommand(_bot),
              //  new FindCommand(_bot, storage),
              //  new Find2Command(_bot, storage),
              //  new ClearCommand(_bot, storage)
            };
                        
            _messageHandlers[MessageType.Text] = new TextMessageHandler(_log, _bot, new DefaultCommand(_bot), commands);
            //_messageHandlers[MessageType.Document] = new DocumentMessageHandler(_log, _bot, parser, storage);

            _queryHandler = new InlineQueryHandler(_log, _bot);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task OnMessage(object sender, MessageEventArgs e)
        {
            _log.Information("Message {messageId} received.", e.Message.MessageId);

            if (_messageHandlers.TryGetValue(e.Message.Type, out var handler))
            {
                try
                {
                    await handler.HandleAsync(e.Message);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, "Message handling error.");
                }
            }
            else
            {
                Log.Warning("Unhandled message {@message}", e.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async Task OnInlineQuery(object sender, InlineQueryEventArgs e)
        {
            _log.Information("Inline query {queryId} received.", e.InlineQuery.Id);

            try
            {
                await _queryHandler.HandleAsync(e.InlineQuery);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Inline query error");
            }
        }

        /// <summary>
        /// But bot infinite loop and receive commands
        /// </summary>
        /// <param name="token"></param>
        public void Run(ManualResetEvent resetEvent)
        {
            _log.Information("Starting the bot...");
            _bot.StartReceiving();
            _log.Information("Bot started. Listening commands...");

            resetEvent.WaitOne();
        }
    }
}

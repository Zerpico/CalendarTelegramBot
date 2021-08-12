using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using CalendarTelegramBot.Commands;
using CalendarTelegramBot.Extensions;

namespace CalendarTelegramBot.Handlers
{
    /// <summary>
    /// Text message handler
    /// </summary>
    public class TextMessageHandler : BaseMessageHandler
    {
        private readonly ILogger _log;
        private readonly ICommand _defaultCmd;
        private readonly ICommand[] _commands;
        private readonly IDictionary<string, ICommand> _commandsDic;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bot">Reference to bot</param>
        /// <param name="defaultCommand">If no commands matched, then default command will be executed</param>
        /// <param name="commands">All possible commands list</param>
        public TextMessageHandler(
            ILogger logger,
            ITelegramBotClient bot,           
            ICommand defaultCommand,
            params ICommand[] commands)
            : base(bot)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            _log = logger.ForContext<TextMessageHandler>();
            _defaultCmd = defaultCommand ?? throw new ArgumentNullException(nameof(defaultCommand));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
            _commandsDic = commands.ToDictionary(c => c.Name);
        }

        /// <inheritdoc />
        public override async Task HandleAsync(Message message)
        {
            _log.Information("Handling a new text message: {message}", message.Text);
                      

            var splitted = message.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var commandName = splitted.Length > 0 ? splitted[0] : string.Empty;
            var commandLine = splitted.Length > 1 ? string.Join(' ', splitted.Skip(1)) : string.Empty;
            commandName = commandName.Split('@')[0];
               
            // getting user info
            var member = await Bot.GetChatMemberAsync(message.Chat.Id, message.From.Id);

            var args = new CommandEventArgs
            {
                Bot = Bot,
                CommandLine = commandLine,
                ChatId = message.Chat.Id,
                UserId = message.From.Id,
                MessageId = message.MessageId,
                IsBotOwner = member.IsBotOwner(),
                IsAdmin = member.IsAdmin()
            };

            _log.Information("Mapping a command '{command}'", commandName);

            if (_commandsDic.TryGetValue(commandName, out var command))
            {
                _log.Information("Command {command} found. Executing...", commandName);
                await command.ExecuteAsync(args);
            }
            else
            {
                // skip non-commands
                //if (!commandName.StartsWith("/"))
                //    return;

                args.CommandLine = message.Text;

                _log.Information("Executing the default command...");
                await _defaultCmd.ExecuteAsync(args);
            }
        }
    }
}

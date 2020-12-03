using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;

namespace CalendarTelegramBot.Commands
{
    /// <summary>
    /// Command arguments
    /// </summary>
    public class CommandEventArgs
    {
        /// <summary>
        /// Bot
        /// </summary>
        public ITelegramBotClient Bot { get; set; }

        /// <summary>
        /// Command arguments WITHOUT command name
        /// </summary>
        public string CommandLine { get; set; }

        /// <summary>
        /// Chat id
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// Message id
        /// </summary>
        public long MessageId { get; set; }

        /// <summary>
        /// Is calling user owner
        /// </summary>
        public bool IsBotOwner { get; set; }

        /// <summary>
        /// Is calling user admin
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}

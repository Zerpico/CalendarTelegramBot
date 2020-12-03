using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace CalendarTelegramBot.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public ITelegramBotClient Bot { get; private set; }

        public abstract string Name { get; }

        protected BaseCommand(ITelegramBotClient bot)
        {
            Bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }

        public abstract Task ExecuteAsync(CommandEventArgs e);
    }
}

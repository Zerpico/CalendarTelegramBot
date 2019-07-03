using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace calendar_flood_bot.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract Task Execute(Message message, TelegramBotClient client);

        public abstract bool Contains(Message message);
    }
}

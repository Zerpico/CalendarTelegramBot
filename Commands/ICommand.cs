using System.Threading.Tasks;
using Telegram.Bot;

namespace CalendarTelegramBot.Commands
{
    /// <summary>
    /// Bot command
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Bot client
        /// </summary>
        ITelegramBotClient Bot { get; }

        /// <summary>
        /// Command name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Command executor
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Task ExecuteAsync(CommandEventArgs e);
    }
}

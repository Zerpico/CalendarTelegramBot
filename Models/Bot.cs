using CalendarTelegramBot.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace CalendarTelegramBot.Models
{
    public class Bot
    {
        private static TelegramBotClient botClient;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (botClient != null)
            {
                return botClient;
            }

            commandsList = new List<Command>();
            commandsList.Add(new StartCommand());
            //commandsList.Add(new SayCommand());
            commandsList.Add(new CalendarCommand());
            commandsList.Add(new LeaveCommand());
            //TODO: Add more commands

            botClient = new TelegramBotClient(BotConfiguration.Token);
            string hook = string.Format(BotConfiguration.Url, "api/message/update");
            await botClient.SetWebhookAsync(hook);
            return botClient;
        }
    }
}

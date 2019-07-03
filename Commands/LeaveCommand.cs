using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace calendar_flood_bot.Commands
{
    public class LeaveCommand : Command
    {
        public override string Name => @"/leave";

        public override bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            return message.Text.Contains(this.Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient)
        {
            await botClient.LeaveChatAsync(message.Chat.Id);
        }
    }
}

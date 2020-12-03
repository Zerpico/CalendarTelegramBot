using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CalendarTelegramBot.Extensions
{
    /// <summary>
    /// ChatMember extension methods
    /// </summary>
    public static class ChatMemberExt
    {
        /// <summary>
        /// Checks, whether chat member is bot owner
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsBotOwner(this ChatMember member)
        {
            if (member == null)
                return false;

            if (string.IsNullOrEmpty(member.User.Username))
                return false;
            return member.User.Username.ToLower() == "zerpico";
        }

        /// <summary>
        /// Checks, whether chat member is bot owner
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsAdmin(this ChatMember member)
        {
            if (member == null)
                return false;

            return member.Status == ChatMemberStatus.Administrator ||
                member.Status == ChatMemberStatus.Creator;
        }
    }
}

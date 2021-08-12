using System;
using System.ComponentModel.DataAnnotations;

namespace CalendarTelegramBot.Context
{
    public class DbLog
    {
        [Key]
        public int Id {get;set;}
        public int ActionType {get;set;}
        public DateTime Dt {get;set;}
        public long UserId  {get;set;}
        public long ChatId  {get;set;}
    }

    public enum ActionType
    {
        onion = 1,
        calendar = 2
    }
}
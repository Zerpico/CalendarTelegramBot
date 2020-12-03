﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalendarTelegramBot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace CalendarTelegramBot.Controllers
{
    [Route("api/message/update")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public async Task<OkResult> Post([FromBody] Update update)
        {
            if (update == null) return Ok();

            var commands = Bot.Commands;
            var message = update.Message;
            var botClient = await Bot.GetBotClientAsync();

            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, botClient);
                    break;
                }
            }
            return Ok();
        }
    }
}

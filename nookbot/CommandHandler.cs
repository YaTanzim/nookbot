using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

namespace nookbot
{
    class CommandHandler
    {
        private DiscordSocketClient client;
        private CommandService service;

        public async Task InitAsync(DiscordSocketClient _client)
        {
            client = _client;
            service = new CommandService();
            await service.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
            client.MessageReceived += HandleCmdAsync;
        }

        private async Task HandleCmdAsync(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message)) return;

            SocketCommandContext context = new SocketCommandContext(client, message);
            int argPos = 0;
            
            if(message.HasStringPrefix(Config.botConfig.commandPrefix, ref argPos))
            {
                IResult result = await service.ExecuteAsync(context, argPos, services: null);

                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}

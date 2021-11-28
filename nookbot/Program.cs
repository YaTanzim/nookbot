using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;

namespace nookbot
{
    class Program
    {
        DiscordSocketClient client;
        CommandHandler cmdHandler;

        static void Main()
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            Console.WriteLine("nookbot 1.0.0");

            if(string.IsNullOrWhiteSpace(Config.botConfig.token))
            {
                Console.WriteLine("The token is empty.");
                return;
            }

            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });

            client.Log += Client_Log;
            await client.LoginAsync(TokenType.Bot, Config.botConfig.token);
            await client.StartAsync();
            cmdHandler = new CommandHandler();
            await cmdHandler.InitAsync(client);
            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage message)
        {
            Console.WriteLine(message.Message);
        }
    }
}

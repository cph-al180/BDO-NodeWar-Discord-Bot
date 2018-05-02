using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DiscoTest.cs.Models;

namespace DiscoTest.cs
{
    public class MyBot
    {
        private DiscordSocketClient client;
        private CommandService commandService;
        private IServiceProvider services;

        static void Main(string[] args) => new MyBot().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            client = new DiscordSocketClient();
            commandService = new CommandService();

            services = new ServiceCollection().AddSingleton(client).AddSingleton(commandService).BuildServiceProvider();

            client.Log += Log;

            await RegisterCommandAsync();

            string token = "NDQxMTYwNDIxMjc0MDI1OTg1.DcsepQ.dQnSM7Tl1mpPj2441zQBmEnEi78";

            await client.LoginAsync(TokenType.Bot, token);

            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {
            client.MessageReceived += HandleCommandAsync;

            await commandService.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if(message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(client, message);

                var result = await commandService.ExecuteAsync(context, argPos, services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }

    }
}

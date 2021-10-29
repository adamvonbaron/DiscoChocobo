using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.Extensions.Configuration;
using System.Reflection;

using Discord.WebSocket;
using Discord.Commands;

namespace DiscoChocobo
{
    public class BotService : IHostedService, IDisposable
    {
        private DiscordSocketClient client { get; }

        private BotConfig botConfig { get; }
        private CommandService commandService { get; }
        private readonly IServiceProvider Services;

        public BotService(IConfiguration configuration, CommandService commands, IServiceProvider services)
        {
            client = new DiscordSocketClient();
            botConfig = configuration.GetSection("DiscoChocobo").Get<BotConfig>();
            commandService = commands;
            Services = services;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            client.Log += Logger.Log;
            client.Ready += Ready;
            client.MessageReceived += HandleCommandAsync;
            await commandService.AddModulesAsync(
                assembly: Assembly.GetEntryAssembly(),
                services: Services
            );
            await client.LoginAsync(Discord.TokenType.Bot, botConfig.DiscordBotToken);
            await client.StartAsync();
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await client.SetStatusAsync(Discord.UserStatus.Offline);
            await client.StopAsync();
            client.Log -= Logger.Log;
            client.Ready -= Ready;
            client.MessageReceived += HandleCommandAsync;
        }

        public async Task Ready()
        {
            await client.SetStatusAsync(Discord.UserStatus.Online);
            await client.SetGameAsync("some chill beats", null, Discord.ActivityType.Listening);
        }

        private async Task HandleCommandAsync(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;

            if (message == null)
                return;

            int argPos = 0;

            // ignore message if not prefixed with command char, if we mention ourselves, or if we are a bot
            if (!(message.HasCharPrefix('!', ref argPos) ||
                message.HasMentionPrefix(client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(client, message);

            // TODO: does this really inject all services from DI?
            await commandService.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: Services
            );
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}

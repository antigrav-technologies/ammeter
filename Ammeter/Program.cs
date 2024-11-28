using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using кансоль = System.Console;

namespace Ammeter;

internal class Program {
    private static readonly string TOKEN = File.ReadAllText("TOKEN.txt");

    private static readonly DiscordSocketClient CLIENT = new(new DiscordSocketConfig() {
        GatewayIntents = GatewayIntents.All,
        UseInteractionSnowflakeDate = false
    });

    private readonly InteractionService interactionService = new(CLIENT.Rest);

    public static Task Main() => new Program().MainAsync();
    
    private async Task MainAsync() {
        await interactionService.AddModuleAsync<CommandModule>(null);
        CLIENT.Log += Log;
        CLIENT.Ready += Ready;
        CLIENT.SlashCommandExecuted += SlashCommandExecuted;
        CLIENT.SelectMenuExecuted += Client_SelectMenuExecuted;
        CLIENT.MessageReceived += Client_MessageReceived;
        CLIENT.ButtonExecuted += InteractionExecuted;
        interactionService.Log += Log;
        await CLIENT.LoginAsync(TokenType.Bot, TOKEN);
        await CLIENT.StartAsync();
        await Task.Delay(-1);
    }
    private async Task<Task> Client_SelectMenuExecuted(SocketMessageComponent cmd) {
        await interactionService.ExecuteCommandAsync(new InteractionContext(CLIENT, cmd, cmd.Channel), null);
        return Task.CompletedTask;
    }

    private async Task<Task> SlashCommandExecuted(SocketSlashCommand cmd) {
        await interactionService.ExecuteCommandAsync(new InteractionContext(CLIENT, cmd, cmd.Channel), null);
        return Task.CompletedTask;
    }

    private async Task<Task> InteractionExecuted(SocketMessageComponent component) {
        string h = component.Data.CustomId;
        return Task.CompletedTask;
    }

    private async Task<Task> Ready() {
        await interactionService.RegisterCommandsGloballyAsync();
        
        кансоль.WriteLine($"@{CLIENT.CurrentUser.Username}#{CLIENT.CurrentUser.Discriminator} is now ready");

        return Task.CompletedTask;
    }

    private async Task<Task> Client_MessageReceived(SocketMessage message) {
        return Task.CompletedTask;
    }

    private static Task Log(LogMessage msg) {
        кансоль.WriteLine(msg);
        return Task.CompletedTask;
    }
}

internal class CommandModule : InteractionModuleBase {
    public InteractionService? Service {get; set;}

    [SlashCommand("ping", "get ping")]
    public async Task PingSlashCommand() {
        await RespondAsync(((DiscordSocketClient)Context.Client).Latency + "ms");
    }
}
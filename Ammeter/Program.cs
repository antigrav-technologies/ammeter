using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using кансоль = System.Console;

// do ropl
/*
def is_online(guild: disnake.Guild, id: int) -> bool:
    if member := guild.get_member(id):
        return (member.status == disnake.Status.offline)
    return False
*/
/*
ukengar = [
    "ТЫ УКЕН ГАР ГАР САМЫЙ НАСТОЯЩИЙ",
    "ТЫ УКЕН ГАР ИЛИ НЕТ??????",
    "ТЫ УКЕН ГАР ТЫ УКЕН ГАР",
    "ТЫ УКЕН КРАН ШАРОВОЙ ОПОРЫ ДЛЯ АРБУЗА 🍍🤔🤔 БАЛШОЙ БАЛШОЙ СИКРЕТ",
    "асексуальность и укен кран 🏗️",
    "ты вообще укен гар",
    "ты не укен гар ты укен кран 🏗️ты укен гар",
    "ты укен гар",
    "ты укен гар и оптимизация детей и не только о себе 😼😼😼",
    "ты укен гар или так и смерть 💀",
    "ты укен гар или укен кран",
    "ты укен гар 😛",
    "ты укен гар 😛 на сайте в разделе новости",
    "ты укен гарантия",
    "ты укен город",
    "ты укен кран для смены воды в скалк 🦠",
    "ты укен кран на это сообщение проверено ✅",
    "ты укен кран шаровой молнии и смерть 💀 и жизнь 🍄🦀 в шкиле 🦈 и солнце ☀️",
    "ты укен кран шаровой опоры для арбузы 🍍",
    "ты укен кран шаровой опоры и солнце ☀️🌤️ в школе 🏫 и смерть и ещё нужен шар из этого списка",
    "ты укен кран шаровый кран шаровой опоры для вас",
    "ты укен кран шаровый кран шаровый кран шаровой молнии и его семья нашли бобров в шкиле 🦈",
    "ты укен магазин",
    "ты укен нар",
    "ты укен тангенс угла",
    "ты укен шар",
    "ты укен 🦂 гар от красного синего цвета не",
    "чо за фигню ты укен кран 🏗️🏗️🏗️🏗️🏗️🏗️🏗️"
]
*/
namespace Ammeter;

internal class Program {
    private static readonly string TOKEN = File.ReadAllText("TOKEN.txt");

    public static readonly DiscordSocketClient CLIENT = new(new DiscordSocketConfig() {
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

        Task.WaitAll(
            Loops.UpdatePresence()
        );
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
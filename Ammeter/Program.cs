using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using кансоль = System.Console;
using static Ammeter.Utils;

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
/* is it really any useful
async def splashes_cycle():
   while True:
       sendbattery = False
       battery = psutil.sensors_battery()
       if battery:
           plugged = battery.power_plugged
           percent = battery.percent

           sendbattery = percent <= 30 and not plugged

       for every in open("splashes_channels.txt").read().split():
           channel = bot.get_channel(int(every))
           if channel is not None:
               if sendbattery:
                   await channel.send(f"tema5002's laptop has {percent}% currently he defenitely should charge it rn")
               else:
                   await channel.send(random.choice(splashes))
               print(f"sending splash on {channel} ({channel.guild})")
           else:
               print(f"cant send splash to channel with id {every}")
               tl.remove_line("splashes_channels.txt", every)
       await asyncio.sleep(600)
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
        string[] h = component.Data.CustomId.Split(";");
        if (h[0] == "?") {
            await component.RespondAsync("Um, sorry Mari but I have no idea what you just said. Did you mean to type that or is your autocorrect acting up? #confusedBlobby", ephemeral: true);
        }
        else if (h[0] == "UPDATELISTEMBED") {
            await component.RespondAsync("блен может ти и прав но я всё равно расопью твоё ипало об камен");
            /*
             *         page_to_go = int(t[1])
               files = os.listdir("shitpost")
               embed = make_list_embed(int(t[1]), files)
               await ctx.response.edit_message(
                   embed=embed,
                   components=make_list_components("UPDATELISTEMBED", page_to_go, files)
               )
             */
        }
        return Task.CompletedTask;
    }

    private async Task<Task> Ready() {
        Log("ready is this a commodore 64 reference :insane::insane::insane::insane::insane::insane:");
        await interactionService.RegisterCommandsGloballyAsync();

        Data.InitializeEmojis();
        
        Log($"@{CLIENT.CurrentUser.Username}#{CLIENT.CurrentUser.Discriminator} is now ready\n");
        Log("Loaded slash commands:\n");
        foreach (var x in interactionService.SlashCommands) {
            Log($"/{x.Name} {string.Join(" ", x.Parameters.Select(p => p.Name))}\n");
        }
        
        Task.WaitAll(
            Loops.RandomlyAppearingRole(),
            Loops.UpdatePresence()
        );
        return Task.CompletedTask;
    }

    private async Task<Task> Client_MessageReceived(SocketMessage message) {
        if (message.Channel is not SocketGuildChannel) return Task.CompletedTask;
        if (GetSCRL(message.Author.Id) switch {
            "2" => true,
            "1" => message.Guild().IsOnline(Data.Users.Abotmin),
            _ => false
        }) await message.AddReactionAsync(Data.Em.staring_cat.Emoji());
        return Task.CompletedTask;
    }

    private static void Log(string msg) => кансоль.WriteLine(msg);

    private static Task Log(LogMessage msg) {
        Log(msg.ToString());
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
using Discord;

namespace Ammeter;

public static class Loops {
    public static async Task UpdatePresence() {
        while (true) {
            var guilds = Program.CLIENT.Guilds!;
            int idiots = guilds.Select(x => x.MemberCount).Sum();

            await Program.CLIENT.SetActivityAsync(new Game($"for {idiots} idiots on {guilds.Count} servers", ActivityType.Watching));
            await Task.Delay(60 * 1000);
        }
    }

    public static async Task RandomlyAppearingRole() {
        var guild = Program.CLIENT.GetGuild(Data.Servers.SlinxAttic);
        var role = guild.GetRole(Data.Roles.SlinxAtticRandomlyAppearingRole);

        while (true) {
            var member = guild.Users.Choice();
            if (member.Roles.Contains(role))
                await member.AddRoleAsync(role);
            else
                await member.RemoveRoleAsync(role);
            await Task.Delay(60 * 1000);
        }
    }
/*
    public static async Task TorturePicardias() {
        var channel = (ISocketMessageChannel)Program.CLIENT.GetChannel(Data.Channels.AmmeterTorturesPicardias)!;

        while (true) {
            await channel.SendMessageAsync(
                "every day, over 5000 picardias have a " +
                string.Join("", Enumerable.Range(0, 100).Select(static _ => Xorshift64.NextChar()))
            );
            await Task.Delay(2 * 60 * 1000);
        }
    }*/
}
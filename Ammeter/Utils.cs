using Discord;
using Discord.WebSocket;

namespace Ammeter;

internal static class Utils {
    public static bool IsOnline(this SocketGuild guild, ulong id) => guild.GetUser(id) is { } member && member.Status != UserStatus.Offline;
    
    public static SocketGuild Guild(this SocketMessage message) => ((SocketGuildChannel)message.Channel).Guild;
    
    public static GuildEmote? GetEmoji(this IGuild guild, string name) => guild.Emotes.FirstOrDefault(e => e.Name == name);
    
    public static string GetFolderPath(IEnumerable<string> args) {
        string folder = "";

        foreach (string f in args) {
            folder = Path.Combine(folder, f);
            if (Directory.Exists(folder)) continue;
            try {
                Directory.CreateDirectory(folder);
            }
            catch (IOException ex) {
                throw new IOException($"Failed to create folders: {ex.Message}", ex);
            }
        }

        return folder;
    }

    public static string GetFilePath(IList<string> args, string? createFile = null) {
        string path = Path.Combine(
            GetFolderPath(args.Take(args.Count - 1)),
            args.Last()
        );

        if (createFile != null && !Path.Exists(path)) {
            File.WriteAllText(path, createFile);
        }

        return path;
    }
    
    public static string GetSCRL(ulong id) => File.ReadAllText(GetFilePath([Data.DATA_PATH, "scrl", $"{id}.txt"], ""));
}
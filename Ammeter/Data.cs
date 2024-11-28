using Discord;

namespace Ammeter;

internal static class Data {
    public static readonly string DATA_PATH = Utils.GetFolderPath(["data"]);

    public static readonly string ЗАТКНИСЬ_КУРИЦА
        = "заткнись курица😤shut up chicken😡πи$daчек прикрыла😋 (я абослют😈)🙀зткнс крца🤐зоткися курапаточка🙄💅З А Т К Н И С Ь🤫К У Р Е Ц А🐓";

    public static readonly ulong[] TEMA5002 = [
        Users.Tema5002, // tema5002
        Users.Cake64, // cake64
        Users.Aromantic // aromantic.
    ];

    public static class Servers {
        public static readonly ulong SlinxAttic = 1042064947867287643;
        public static readonly ulong NosokServer = 1305161389186486377;
    }

    public static class Users {
        public static readonly ulong Tema5002 = 558979299177136164;
        public static readonly ulong Cake64 = 1204799892988629054;
        public static readonly ulong Aromantic = 1202544285287718927;
        public static readonly ulong Abotmin = 1204295911367512084;
    }

    public static class Roles {
        public static readonly ulong SlinxAtticRandomlyAppearingRole = 1260262067886100480;
    }

    public static class Channels {
        // ...
    }

    public enum Em {
        staring_cat
    }

    private static Dictionary<Em, IEmote> Emojis = []; // apparently it cant be initialized before the bot is ready

    public static IEmote Emoji(this Em e) => Emojis[e];
    
    public static void InitializeEmojis() {
        Emojis[Em.staring_cat] = Program.CLIENT.GetGuild(Servers.NosokServer).GetEmoji("staring_cat")!;
    }
}
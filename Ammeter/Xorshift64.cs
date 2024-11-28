namespace Ammeter;

public static class Xorshift64 {
    private static ulong state = (ulong)(new Random().Next(int.MaxValue) * int.MaxValue + new Random().Next(int.MaxValue));

    private static ulong Next() {
        state ^= state << 7;
        state ^= state >> 9;
        return state;
    }

    public static ulong NextUint16() => (ushort)Next();

    public static char NextChar() => (char)NextUint16();

    public static int NextInt32() => (int)Next();

    public static int NextInt32(int a, int b) => a + NextInt32() % (b - a);

    public static int NextInt32(int a) => NextInt32(0, a);

    public static T Choice<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(NextInt32(enumerable.Count()));
}
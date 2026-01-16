using System;

namespace Triangulum.Australe;

public static class Numbers
{
    public static i64 MinOfInt(u8 bits) => bits != 0 && bits <= 64 ? -1L << (bits - 1) : throw new ArgumentOutOfRangeException(nameof(bits));
    public static iLG MinOfInt(u16 bits) => bits != 0 ? -iLG.One << (bits - 1) : throw new ArgumentOutOfRangeException(nameof(bits));

    public static i64 MaxOfInt(u8 bits) => bits != 0 && bits <= 64 ? (1L << (bits - 1)) - 1L : throw new ArgumentOutOfRangeException(nameof(bits));
    public static iLG MaxOfInt(u16 bits) => bits != 0 ? (iLG.One << (bits - 1)) - 1 : throw new ArgumentOutOfRangeException(nameof(bits));

    public static u64 MaxOfUInt(u8 bits)
    {
        if (bits == 0 || bits > 64)
        {
            throw new ArgumentOutOfRangeException(nameof(bits));
        }

        // If using 64 bits, bit shift operations will cause the returned value to overflow,
        // so return the maximum value of a 64-bit unsigned integer instead.
        return bits != 64 ? (1UL << bits) - 1UL : u64.MaxValue;
    }
    public static iLG MaxOfUInt(u16 bits) => bits != 0 ? (iLG.One << bits) - 1 : throw new ArgumentOutOfRangeException(nameof(bits));
}

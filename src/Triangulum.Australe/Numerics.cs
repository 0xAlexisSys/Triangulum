using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace Triangulum.Australe;

public static class Numerics
{
	private const f32 Float32EpsilonFunc1 = 1.0F;
	private const f32 Float32EpsilonFunc2 = 2.0F;
	private const f64 Float64EpsilonFunc1 = 1.0D;
	private const f64 Float64EpsilonFunc2 = 2.0D;
	private const f128 Float128EpsilonFunc1 = 1.0M;
	private const f128 Float128EpsilonFunc2 = 2.0M;

	private static readonly f16 s_float16EpsilonFunc1 = (f16)1.0F;
	private static readonly f16 s_float16EpsilonFunc2 = (f16)1.0F;

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

    public static f16 Float16Epsilon()
    {
		f16 machineEpsilon = s_float16EpsilonFunc1;
		f16 machineEpsilonLast = new();

		while (s_float16EpsilonFunc1 + machineEpsilon != s_float16EpsilonFunc1)
		{
			machineEpsilonLast = machineEpsilon;
			machineEpsilon /= s_float16EpsilonFunc2;
		}
	    return machineEpsilonLast;
    }
    public static f32 Float32Epsilon()
    {
	    f32 machineEpsilon = Float32EpsilonFunc1;
	    f32 machineEpsilonLast = 0.0F;

	    while (Float32EpsilonFunc1 + machineEpsilon != Float32EpsilonFunc1)
	    {
		    machineEpsilonLast = machineEpsilon;
		    machineEpsilon /= Float32EpsilonFunc2;
	    }
	    return machineEpsilonLast;
    }
}

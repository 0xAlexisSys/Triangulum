namespace Triangulum.Australe.Extensions;

public static partial class CSharpExtensionMembers
{
	private const f32 Float32Epsilon = 0.0F;
	private const f64 Float64Epsilon = 0.0D;
	private const f128 Float128Epsilon = 0.0M;

	private static readonly f16 s_float16Epsilon = (f16)0.0F;

	// public static bool ApproxEqual(this f16 value, f16 otherValue) => f16.Abs(value - otherValue) < (f16)FloatEpsilon;
	// public static bool ApproxEqual(this f32 value, f32 otherValue) => f32.Abs(value - otherValue) < (f32)FloatEpsilon;
	// public static bool ApproxEqual(this f64 value, f64 otherValue) => f64.Abs(value - otherValue) < FloatEpsilon;
	// public static bool ApproxEqual(this f128 value, f128 otherValue) => f128.Abs(value - otherValue) < (f128)FloatEpsilon;

	// ReSharper disable CompareOfFloatsByEqualityOperator
    public static bool HasDecimals(this f16 value) => value != f16.Truncate(value);
    public static bool HasDecimals(this f32 value) => value != f32.Truncate(value);
    public static bool HasDecimals(this f64 value) => value != f64.Truncate(value);
    public static bool HasDecimals(this f128 value) => value != f128.Truncate(value);
    // ReSharper restore CompareOfFloatsByEqualityOperator

    //public static bool HasDecimalsEA(this f16 value) => va
}

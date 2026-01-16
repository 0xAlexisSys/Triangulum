namespace Triangulum.Australe.Extensions;

public static partial class CSharpExtensionMembers
{
    public static bool HasDecimals(this f16 value) => value != f16.Truncate(value);

    // ReSharper disable once CompareOfFloatsByEqualityOperator
    public static bool HasDecimals(this f32 value) => value != f32.Truncate(value);

    // ReSharper disable once CompareOfFloatsByEqualityOperator
    public static bool HasDecimals(this f64 value) => value != f64.Truncate(value);

    public static bool HasDecimals(this f128 value) => value != f128.Truncate(value);
}

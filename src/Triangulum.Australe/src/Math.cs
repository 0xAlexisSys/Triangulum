using System;
using System.Data;
using Triangulum.Australe.Extensions;

namespace Triangulum.Australe;

public static class Math
{
    private static readonly DataTable s_dataTable = new();

    public static f16 Snap(f16 value, f16 step) => f16.Round(value / step) * step;
    public static f32 Snap(f32 value, f32 step) => f32.Round(value / step) * step;
    public static f64 Snap(f64 value, f64 step) => f64.Round(value / step) * step;
    #if NET7_0_OR_GREATER
    public static f128 Snap(f128 value, f128 step) => f128.Round(value / step) * step;
    #endif

    public static bool TryEvaluateNumberExpression(string expression, out NumberExprResult result)
    {
        #if NET7_0_OR_GREATER
        result = 0.0M;
        #else
        result = 0.0D;
        #endif

        if (expression.IsWhiteSpace()) return false;

        try
        {
            object computed = s_dataTable.Compute(expression, null);
            return computed != DBNull.Value && NumberExprResult.TryParse(computed.ToString(), out result);
        }
        catch
        {
            return false;
        }
    }
}

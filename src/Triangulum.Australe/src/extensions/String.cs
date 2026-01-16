namespace Triangulum.Australe.Extensions;

public static partial class CSharpExtensionMembers
{
    public static bool IsEmpty(this string @string) => @string.Length == 0;

    public static bool IsWhiteSpace(this string @string) => @string.Trim().Length == 0;

    public static string CharAt(this string @string, i32 index) => @string[index].ToString();
}

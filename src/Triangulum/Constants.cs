using Godot;

namespace Triangulum;

internal static class Enum
{
    public enum ThemeID : u8
    {
        Triangulum,
        Empty,
    }
}

internal static class Const
{
    public const string IconsPath = "res://assets/icons";
    public const string ThemesPath = "res://assets/themes";

    public static readonly Theme?[] Themes = [
        ResourceLoader.Load<Theme>($"{ThemesPath}/Triangulum.tres"),
        null,
    ];
    public static readonly GDVector2 ComponentInputMinSize = new((RealT)0.0, (RealT)36.0);

	/// <summary>
	/// Cached <see cref="StringName"/>s for groups, for fast lookup.
	/// </summary>
    public static class GroupName
    {
        public static readonly StringName Components = "Components";
    }
}

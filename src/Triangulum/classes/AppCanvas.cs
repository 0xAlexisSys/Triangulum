using Godot;

namespace Triangulum.Classes;

[GlobalClass, Icon($"{IconsPath}/2DNodes.svg"), Tool]
internal partial class AppCanvas : CanvasLayer
{
    [Export]
    public ThemeID ThemeID
    {
        get => _themeID;
        set
        {
            _themeID = value;
            if (IsNodeReady())
            {
                foreach (Node node in GetTree().GetNodesInGroup(GroupName.Components)) SetComponentTheme(node);
            }
        }
    }
    private ThemeID _themeID = ThemeID.Triangulum;

    private AppCanvas()
    {
        ChildEnteredTree += node =>
        {
            if (node.IsInGroup(GroupName.Components)) SetComponentTheme(node);
        };
    }

    private void SetComponentTheme(Node node)
    {
        if (node is Component component) component.Theme = Themes[(i32)_themeID];
    }
}

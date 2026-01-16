using Godot;
using Triangulum.Classes;
using Triangulum.Australe.Extensions;

namespace Triangulum.Components;

[Tool, GlobalClass, Icon($"{IconsPath}/TextEdit.svg")]
internal partial class TextArea : Component
{
    public const i64 InputBoxWrapLinesDisabled = (i64)TextEdit.LineWrappingMode.None;
    public const i64 InputBoxWrapLinesEnabled = (i64)TextEdit.LineWrappingMode.Boundary;

    public VBoxContainer? NContainer { get; private set; }
    public Label? NLabel { get; private set; }
    public TextEdit? NInputBox { get; private set; }

    public TextArea() : base()
    {
        #if TOOLS
        if (IsInGroup(GroupName.Components)) return;
        #endif

        NContainer = new();
        SetNodeIdentifier(NContainer, "Container");
        AddInternalChild(NContainer, this);

        NLabel = new()
        {
            Visible = !LabelText.IsEmpty(),
        };
        SetNodeIdentifier(NLabel, "Label");
        AddInternalChild(NLabel, NContainer);

        NInputBox = new()
        {
            WrapMode = !InputBoxWrapLines ? TextEdit.LineWrappingMode.None : TextEdit.LineWrappingMode.Boundary,
            CustomMinimumSize = StringEditMinSize,
            SizeFlagsVertical = SizeFlags.ExpandFill,
        };
        SetNodeIdentifier(NInputBox, "InputBox");
        NInputBox.TextChanged += () => Value = NInputBox.Text;
        AddInternalChild(NInputBox, NContainer);
    }
}

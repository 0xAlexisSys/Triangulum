using Godot;
using Triangulum.Classes;
using Triangulum.Australe.Extensions;

namespace Triangulum.Components;

[GlobalClass, Icon($"{IconsPath}/LineEdit.svg"), Tool]
internal partial class TextBox : Component
{
    public const i32 MaxLengthInfinity = 0;
    public const i32 InputBoxSecretCharacterLength = 1;

    public VBoxContainer? NContainer { get; private set; }
    public Label? NLabel { get; private set; }
    public LineEdit? NInputBox { get; private set; }

    public TextBox() : base()
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
            KeepEditingOnTextSubmit = true,
            CustomMinimumSize = ComponentInputMinSize,
            SizeFlagsVertical = SizeFlags.ExpandFill,
        };
        SetNodeIdentifier(NInputBox, "InputBox");
        NInputBox.TextChanged += newText => Value = newText;
        AddInternalChild(NInputBox, NContainer);
    }
}

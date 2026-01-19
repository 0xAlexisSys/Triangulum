using Godot;

namespace Triangulum.Components;

internal partial class TextArea
{
	#region Main
    /// <summary>
    /// Current value.
    /// </summary>
    [Export(PropertyHint.MultilineText)]
    public string Value { get; set => SetProperty(ref field, value); } = string.Empty;

    /// <summary>
    /// If <c>false</c>, the input box is not editable by the user.
    /// </summary>
    [Export]
    public bool Enabled { get; set => SetProperty(ref field, value); } = true;

    /// <summary>
    /// A tooltip that is displayed when the input box is hovered over for a few moments.
    /// </summary>
    [Export(PropertyHint.MultilineText)]
    public string Tooltip { get; set => SetProperty(ref field, value); } = string.Empty;
    #endregion // Main
    #region Label
    /// <summary>
    /// Plain text displayed on the label. If <c>""</c>, the label is not visible.
    /// </summary>
    [ExportGroup("Label", "Label"), Export]
    public string LabelText { get; set => SetProperty(ref field, value); } = string.Empty;

    /// <summary>
    /// If <c>true</c>, the label's text is rendered as <b>UPPERCASE</b>.
    /// </summary>
    [Export]
    public bool LabelUppercase { get; set => SetProperty(ref field, value); } = false;

    /// <summary>
    /// Controls the alignment of the label's text.
    /// </summary>
    [Export]
    public HorizontalAlignment LabelAlignment { get; set => SetProperty(ref field, value); } = HorizontalAlignment.Left;
    #endregion // Label
    #region Input Box
    /// <summary>
    /// Text to display on the input box when it is empty.
    /// </summary>
    [ExportGroup("Input Box", "InputBox"), Export]
    public string InputBoxPlaceholder { get; set => SetProperty(ref field, value); } = string.Empty;

    /// <summary>
    /// If <c>true</c>, lines are wrapped when they exceed the input box's width.
    /// </summary>
    [Export]
    public bool InputBoxWrapLines { get; set => SetProperty(ref field, value); } = true;

    /// <summary>
    /// Controls how text is automatically wrapped when <see cref="InputBoxWrapLines"/>
    /// is <c>true</c>.
    /// </summary>
    [Export]
    public TextServer.AutowrapMode InputBoxAutowrapMode { get; set => SetProperty(ref field, value); } = TextServer.AutowrapMode.WordSmart;

    /// <summary>
    /// If <c>true</c>, wrapped lines are indented to visually distinguish them from the
    /// first line.
    /// </summary>
    [Export]
    public bool InputBoxIndentWrappedLines { get; set => SetProperty(ref field, value); } = false;

    /// <summary>
    /// If <c>true</c>, drag and drop of text is enabled in the input box.
    /// </summary>
    [Export]
    public bool InputBoxDragAndDropTextEnabled { get; set => SetProperty(ref field, value); } = true;

    /// <summary>
    /// If <c>false</c>, the input box cannot be selected by the user.
    /// </summary>
    [Export]
    public bool InputBoxSelectable { get; set => SetProperty(ref field, value); } = true;

    /// <summary>
    /// If <c>true</c>, all selected text in the input box are deselected when focus is
    /// lost.
    /// </summary>
    [Export]
    public bool InputBoxDeselectOnFocusLoss { get; set => SetProperty(ref field, value); } = true;
    #endregion // Input Box
    #region Input Box > Context Menu
    /// <summary>
    /// If <c>true</c>, right-clicking the input box opens the context menu.
    /// </summary>
    [ExportSubgroup("Context Menu", "ContextMenu"), Export]
    public bool ContextMenuEnabled { get; set => SetProperty(ref field, value); } = true;

    /// <summary>
    /// If <c>true</c>, the Emoji and Symbols menu is accessible in the context menu.
    /// </summary>
    [Export]
    public bool ContextMenuEmojiAndSymbolsMenuEnabled { get; set => SetProperty(ref field, value); } = true;

    /// <summary>
    /// If <c>true</c>, shortcut keys for context menu items are enabled, even if
    /// <see cref="ContextMenuEnabled"/> is <c>false</c>.
    /// </summary>
    [Export]
    public bool ContextMenuShortcutKeysEnabled { get; set => SetProperty(ref field, value); } = true;
    #endregion // Input Box > Context Menu
    #region Input Box > Virtual Keyboard
    /// <summary>
    /// If <c>true</c>, the native virtual keyboard is enabled on platforms that support
    /// it.
    /// </summary>
    [ExportSubgroup("Virtual Keyboard", "VirtualKeyboard"), Export]
    public bool VirtualKeyboardEnabled { get; set => SetProperty(ref field, value); } = true;

    /// <summary>
    /// If <c>true</c>, the native virtual keyboard is shown on focus events on platforms
    /// that support it.
    /// </summary>
    [Export]
    public bool VirtualKeyboardShowOnFocus { get; set => SetProperty(ref field, value); } = true;
    #endregion // Input Box > Virtual Keyboard
}

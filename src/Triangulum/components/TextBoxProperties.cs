using Godot;

namespace Triangulum.Components;

internal partial class TextBox
{
	#region Main
	/// <summary>
	/// Current text value.
	/// </summary>
	[Export]
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

	/// <summary>
	/// Maximum length of the text value. If <c>0</c>, there is no limit.
	/// </summary>
	/// <remarks>
	/// Minimum is <c>0</c> (<see cref="MaxLengthInfinity"/>).
	/// </remarks>
	[Export]
	public i32 MaxLength { get; set => SetProperty(ref field, value); } = MaxLengthInfinity;
	#endregion // Main
	#region Label
	/// <summary>
	/// Plain text displayed on the label. If <c>""</c>, the label is not visible.
	/// </summary>
	[ExportGroup("Label", "Label"), Export]
	public string LabelText { get; set => SetProperty(ref field, value); } = string.Empty;

	/// <summary>
	/// If <c>true</c>, the label's text is always rendered as <b>UPPERCASE</b>.
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
	/// If not <c>""</c>, the input box's text is rendered with
	/// <see cref="InputBoxSecretCharacter"/>.
	/// </summary>
	/// <remarks>
	/// Only one character is permitted.
	/// </remarks>
	[Export(PropertyHint.PlaceholderText, "*")]
	public string InputBoxSecretCharacter { get; set => SetProperty(ref field, value); } = string.Empty;

	/// <summary>
	/// If <c>true</c>, a clear button is displayed when the input box is not empty.
	/// </summary>
	[Export]
	public bool InputBoxClearButtonEnabled { get; set => SetProperty(ref field, value); } = false;

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

	/// <summary>
	/// Controls the alignment of the input box's text.
	/// </summary>
	[Export]
	public HorizontalAlignment InputBoxAlignment { get; set => SetProperty(ref field, value); } = HorizontalAlignment.Left;
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
	/// Specifies the type of virtual keyboard to show.
	/// </summary>
	[Export]
	public LineEdit.VirtualKeyboardTypeEnum VirtualKeyboardType { get; set => SetProperty(ref field, value); } = LineEdit.VirtualKeyboardTypeEnum.Default;

	/// <summary>
	/// If <c>true</c>, the native virtual keyboard is shown on focus events on platforms
	/// that support it.
	/// </summary>
	[Export]
	public bool VirtualKeyboardShowOnFocus { get; set => SetProperty(ref field, value); } = true;
	#endregion // Input Box > Virtual Keyboard
}

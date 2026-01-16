using Godot;

namespace Triangulum.Components;

internal partial class TextBox
{
	#region Main
	/// <summary>
	/// Current text value.
	/// </summary>
	[Export]
	public string Value { get => _value; set => SetProperty(ref _value, value); }
	private string _value = string.Empty;

	/// <summary>
	/// If <c>false</c>, the input box is not editable by the user.
	/// </summary>
	[Export]
	public bool Enabled { get => _enabled; set => SetProperty(ref _enabled, value); }
	private bool _enabled = true;

	/// <summary>
	/// A tooltip that is displayed when the input box is hovered over for a few moments.
	/// </summary>
	[Export(PropertyHint.MultilineText)]
	public string Tooltip { get => _tooltip; set => SetProperty(ref _tooltip, value); }
	private string _tooltip = string.Empty;

	/// <summary>
	/// Maximum length of the text value. If <c>0</c>, there is no limit.
	/// </summary>
	[Export]
	public i32 MaxLength { get => _maxLength; set => SetProperty(ref _maxLength, value); }
	private i32 _maxLength = MaxLengthInfinity;
	#endregion // Main
	#region Label
	/// <summary>
	/// Plain text displayed on the label. If <c>""</c>, the label is not visible.
	/// </summary>
	[ExportGroup(name: "Label", prefix: "Label"), Export]
	public string LabelText { get => _labelText; set => SetProperty(ref _labelText, value); }
	private string _labelText = string.Empty;

	/// <summary>
	/// If <c>true</c>, the label's text is always rendered as <b>UPPERCASE</b>.
	/// </summary>
	[Export]
	public bool LabelUppercase { get => _labelUppercase; set => SetProperty(ref _labelUppercase, value); }
	private bool _labelUppercase = false;

	/// <summary>
	/// Controls the alignment of the label's text.
	/// </summary>
	[Export]
	public HorizontalAlignment LabelAlignment { get => _labelAlignment; set => SetProperty(ref _labelAlignment, value); }
	private HorizontalAlignment _labelAlignment = HorizontalAlignment.Left;
	#endregion // Label
	#region Input Box
	/// <summary>
	/// Text to display on the input box when it is empty.
	/// </summary>
	[ExportGroup(name: "Input Box", prefix: "InputBox"), Export]
	public string InputBoxPlaceholder { get => _inputBoxPlaceholder; set => SetProperty(ref _inputBoxPlaceholder, value); }
	private string _inputBoxPlaceholder = string.Empty;

	/// <summary>
	/// If not <c>""</c>, the input box's text is rendered with
	/// <see cref="InputBoxSecretCharacter"/>.
	/// </summary>
	/// <remarks>
	/// Only one character is permitted.
	/// </remarks>
	[Export(PropertyHint.PlaceholderText, "*")]
	public string InputBoxSecretCharacter { get => _inputBoxSecretCharacter; set => SetProperty(ref _inputBoxSecretCharacter, value); }
	private string _inputBoxSecretCharacter = string.Empty;

	/// <summary>
	/// If <c>true</c>, a clear button is displayed when the input box is not empty.
	/// </summary>
	[Export]
	public bool InputBoxClearButtonEnabled { get => _inputBoxClearButtonEnabled; set => SetProperty(ref _inputBoxClearButtonEnabled, value); }
	private bool _inputBoxClearButtonEnabled = false;

	/// <summary>
	/// If <c>false</c>, the input box is not selectable by the user.
	/// </summary>
	[Export]
	public bool InputBoxSelectable { get => _inputBoxSelectable; set => SetProperty(ref _inputBoxSelectable, value); }
	private bool _inputBoxSelectable = true;

	/// <summary>
	/// If <c>true</c>, all selected text in the input box are deselected when focus is
	/// lost.
	/// </summary>
	[Export]
	public bool InputBoxDeselectOnFocusLoss { get => _inputBoxDeselectOnFocusLoss; set => SetProperty(ref _inputBoxDeselectOnFocusLoss, value); }
	private bool _inputBoxDeselectOnFocusLoss = true;

	/// <summary>
	/// Controls the alignment of the input box's text.
	/// </summary>
	[Export]
	public HorizontalAlignment InputBoxAlignment { get => _inputBoxAlignment; set => SetProperty(ref _inputBoxAlignment, value); }
	private HorizontalAlignment _inputBoxAlignment = HorizontalAlignment.Left;
	#endregion // Input Box
	#region Input Box > Context Menu
	/// <summary>
	/// If <c>true</c>, right-clicking the input box opens the context menu.
	/// </summary>
	[ExportSubgroup(name: "Context Menu", prefix: "ContextMenu"), Export]
	public bool ContextMenuEnabled { get => _contextMenuEnabled; set => SetProperty(ref _contextMenuEnabled, value); }
	private bool _contextMenuEnabled = true;

	/// <summary>
	/// If <c>true</c>, the Emoji and Symbols menu is accessible in the context menu.
	/// </summary>
	[Export]
	public bool ContextMenuEmojiAndSymbolsMenuEnabled { get => _contextMenuEmojiAndSymbolsMenuEnabled; set => SetProperty(ref _contextMenuEmojiAndSymbolsMenuEnabled, value); }
	private bool _contextMenuEmojiAndSymbolsMenuEnabled = true;

	/// <summary>
	/// If <c>true</c>, shortcut keys for context menu items are enabled, even if
	/// <see cref="ContextMenuEnabled"/> is <c>false</c>.
	/// </summary>
	[Export]
	public bool ContextMenuShortcutKeysEnabled { get => _contextMenuShortcutKeysEnabled; set => SetProperty(ref _contextMenuShortcutKeysEnabled, value); }
	private bool _contextMenuShortcutKeysEnabled = true;
	#endregion // Input Box > Context Menu
	#region Input Box > Virtual Keyboard

	/// <summary>
	/// If <c>true</c>, the native virtual keyboard is enabled on platforms that support
	/// it.
	/// </summary>
	[ExportSubgroup(name: "Virtual Keyboard", prefix: "VirtualKeyboard"), Export]
	public bool VirtualKeyboardEnabled { get => _virtualKeyboardEnabled; set => SetProperty(ref _virtualKeyboardEnabled, value); }
	private bool _virtualKeyboardEnabled = true;

	/// <summary>
	/// Specifies the type of virtual keyboard to show.
	/// </summary>
	[Export]
	public LineEdit.VirtualKeyboardTypeEnum VirtualKeyboardType { get => _virtualKeyboardType; set => SetProperty(ref _virtualKeyboardType, value); }
	private LineEdit.VirtualKeyboardTypeEnum _virtualKeyboardType = LineEdit.VirtualKeyboardTypeEnum.Default;

	/// <summary>
	/// If <c>true</c>, the native virtual keyboard is shown on focus events on platforms
	/// that support it.
	/// </summary>
	[Export]
	public bool VirtualKeyboardShowOnFocus { get => _virtualKeyboardShowOnFocus; set => SetProperty(ref _virtualKeyboardShowOnFocus, value); }
	private bool _virtualKeyboardShowOnFocus = true;
	#endregion // Input Box > Virtual Keyboard
}

using Godot;
using Triangulum.Australe.Extensions;

namespace Triangulum.Components;

internal partial class TextBox
{
	#region Main
	private string ModifyNewValue(string value) => MaxLength != MaxLengthInfinity && value.Length > MaxLength ? value[0..MaxLength] : value;

	private void PostSetValue()
	{
		if (NInputBox != null && NInputBox.Text != Value) NInputBox.Text = Value;
	}

	private void PostSetEnabled() => NInputBox?.Set(LineEdit.PropertyName.Editable, Enabled);

	private void PostSetTooltip() => NInputBox?.Set(Control.PropertyName.TooltipText, Tooltip);

	private static i32 ModifyNewMaxLength(i32 value) => i32.Max(value, 0);

	private void PreSetMaxLength(i32 value)
	{
		if (value != MaxLengthInfinity && Value.Length > value) Value = Value[0..value];
	}

	private void PostSetMaxLength() => NInputBox?.Set(LineEdit.PropertyName.MaxLength, MaxLength);
	#endregion // Main
	#region Label
	private void PostSetLabelText()
	{
		if (NLabel != null)
		{
			NLabel.Visible = !LabelText.IsEmpty();
			NLabel.Text = LabelText;
		}
	}

	private void PostSetLabelUppercase() => NLabel?.Set(Label.PropertyName.Uppercase, LabelUppercase);

	private void PostSetLabelAlignment() => NLabel?.Set(Label.PropertyName.HorizontalAlignment, (i64)LabelAlignment);
	#endregion // Label
	#region Input Box
	private void PostSetInputBoxPlaceholder() => NInputBox?.Set(LineEdit.PropertyName.PlaceholderText, InputBoxPlaceholder);

	private static string ModifyNewInputBoxSecretCharacter(string value)
	{
		// If the current context is the editor and the secret character's length is over 1,
		// use its second character (mainly for making it easy to edit in the inspector).
		if (value.Length > InputBoxSecretCharacterLength) value = value.CharAt(!Engine.IsEditorHint() ? 0 : 1);

		return value;
	}

	private void PostSetInputBoxSecretCharacter()
	{
		if (NInputBox != null)
		{
			NInputBox.Secret = InputBoxSecretCharacter.Length == InputBoxSecretCharacterLength;
			NInputBox.SecretCharacter = InputBoxSecretCharacter;
		}
	}

	private void PostSetInputBoxClearButtonEnabled() => NInputBox?.Set(LineEdit.PropertyName.ClearButtonEnabled, InputBoxClearButtonEnabled);

	private void PostSetInputBoxSelectable() => NInputBox?.Set(LineEdit.PropertyName.SelectingEnabled, InputBoxSelectable);

	private void PostSetInputBoxDeselectOnFocusLoss() => NInputBox?.Set(LineEdit.PropertyName.DeselectOnFocusLossEnabled, InputBoxDeselectOnFocusLoss);

	private void PostSetInputBoxAlignment() => NInputBox?.Set(LineEdit.PropertyName.Alignment, (i64)InputBoxAlignment);
	#endregion // Input Box
	#region Input Box > Context Menu
	private void PostSetContextMenuEnabled() => NInputBox?.Set(LineEdit.PropertyName.ContextMenuEnabled, ContextMenuEnabled);

	private void PostSetContextMenuEmojiAndSymbolsMenuEnabled() => NInputBox?.Set(LineEdit.PropertyName.EmojiMenuEnabled, ContextMenuEmojiAndSymbolsMenuEnabled);

	private void PostSetContextMenuShortcutKeysEnabled() => NInputBox?.Set(LineEdit.PropertyName.ShortcutKeysEnabled, ContextMenuShortcutKeysEnabled);
	#endregion // Input Box > Context Menu
	#region Input Box > Virtual Keyboard
	private void PostSetVirtualKeyboardEnabled() => NInputBox?.Set(LineEdit.PropertyName.VirtualKeyboardEnabled, VirtualKeyboardEnabled);

	private void PostSetVirtualKeyboardType() => NInputBox?.Set(LineEdit.PropertyName.VirtualKeyboardType, (i64)VirtualKeyboardType);

	private void PostSetVirtualKeyboardShowOnFocus() => NInputBox?.Set(LineEdit.PropertyName.VirtualKeyboardShowOnFocus, VirtualKeyboardShowOnFocus);
	#endregion // Input Box > Virtual Keyboard
}

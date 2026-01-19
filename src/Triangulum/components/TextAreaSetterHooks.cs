using Godot;
using Triangulum.Australe.Extensions;

namespace Triangulum.Components;

internal partial class TextArea
{
	#region Main
	private void PostSetValue()
	{
		if (NInputBox != null && NInputBox.Text != Value)
		{
			NInputBox.Text = Value;
		}
	}

	private void PostSetEnabled() => NInputBox?.Editable = Enabled;

	private void PostSetTooltip() => NInputBox?.TooltipText = Tooltip;
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

	private void PostSetLabelUppercase() => NLabel?.Uppercase = LabelUppercase;

	private void PostSetLabelAlignment() => NLabel?.HorizontalAlignment = LabelAlignment;
	#endregion // Label
	#region Input Box
	private void PostSetInputBoxPlaceholder() => NInputBox?.PlaceholderText = InputBoxPlaceholder;

	private void PostSetInputBoxWrapLines() => NInputBox?.WrapMode = !InputBoxWrapLines ? TextEdit.LineWrappingMode.None : TextEdit.LineWrappingMode.Boundary;

	private void PostSetInputBoxAutowrapMode() => NInputBox?.AutowrapMode = InputBoxAutowrapMode;

	private void PostSetInputBoxIndentWrappedLines() => NInputBox?.IndentWrappedLines = InputBoxIndentWrappedLines;

	private void PostSetInputBoxDragAndDropTextEnabled() => NInputBox?.DragAndDropSelectionEnabled = InputBoxDragAndDropTextEnabled;

	private void PostSetInputBoxSelectable() => NInputBox?.SelectingEnabled = InputBoxSelectable;

	private void PostSetInputBoxDeselectOnFocusLoss() => NInputBox?.DeselectOnFocusLossEnabled = InputBoxDeselectOnFocusLoss;
	#endregion // Input Box
	#region Input Box > Context Menu
	private void PostSetContextMenuEnabled() => NInputBox?.ContextMenuEnabled = ContextMenuEnabled;

	private void PostSetContextMenuEmojiAndSymbolsMenuEnabled() => NInputBox?.EmojiMenuEnabled = ContextMenuEmojiAndSymbolsMenuEnabled;

	private void PostSetContextMenuShortcutKeysEnabled() => NInputBox?.ShortcutKeysEnabled = ContextMenuShortcutKeysEnabled;
	#endregion // Input Box > Context Menu
	#region Input Box > Virtual Keyboard
	private void PostSetVirtualKeyboardEnabled() => NInputBox?.VirtualKeyboardEnabled = VirtualKeyboardEnabled;

	private void PostSetVirtualKeyboardShowOnFocus() => NInputBox?.VirtualKeyboardShowOnFocus = VirtualKeyboardShowOnFocus;
	#endregion // Input Box > Virtual Keyboard
}

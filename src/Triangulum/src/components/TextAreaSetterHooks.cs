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

	private void PostSetEnabled() => NInputBox?.Set(TextEdit.PropertyName.Editable, Enabled);

	private void PostSetTooltip() => NInputBox?.Set(Control.PropertyName.TooltipText, Tooltip);
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
	private void PostSetInputBoxPlaceholder() => NInputBox?.Set(TextEdit.PropertyName.PlaceholderText, InputBoxPlaceholder);

	private void PostSetInputBoxWrapLines() => NInputBox?.Set(TextEdit.PropertyName.WrapMode, !InputBoxWrapLines ? InputBoxWrapLinesDisabled : InputBoxWrapLinesEnabled);

	private void PostSetInputBoxAutowrapMode() => NInputBox?.Set(TextEdit.PropertyName.AutowrapMode, (i64)InputBoxAutowrapMode);

	private void PostSetInputBoxIndentWrappedLines() => NInputBox?.Set(TextEdit.PropertyName.IndentWrappedLines, InputBoxIndentWrappedLines);

	private void PostSetInputBoxDragAndDropTextEnabled() => NInputBox?.Set(TextEdit.PropertyName.DragAndDropSelectionEnabled, InputBoxDragAndDropTextEnabled);

	private void PostSetInputBoxSelectable() => NInputBox?.Set(TextEdit.PropertyName.SelectingEnabled, InputBoxSelectable);

	private void PostSetInputBoxDeselectOnFocusLoss() => NInputBox?.Set(TextEdit.PropertyName.DeselectOnFocusLossEnabled, InputBoxDeselectOnFocusLoss);
	#endregion // Input Box
	#region Input Box > Context Menu
	private void PostSetContextMenuEnabled() => NInputBox?.Set(TextEdit.PropertyName.ContextMenuEnabled, ContextMenuEnabled);

	private void PostSetContextMenuEmojiAndSymbolsMenuEnabled() => NInputBox?.Set(TextEdit.PropertyName.EmojiMenuEnabled, ContextMenuEmojiAndSymbolsMenuEnabled);

	private void PostSetContextMenuShortcutKeysEnabled() => NInputBox?.Set(TextEdit.PropertyName.ShortcutKeysEnabled, ContextMenuShortcutKeysEnabled);
	#endregion // Input Box > Context Menu
	#region Input Box > Virtual Keyboard
	private void PostSetVirtualKeyboardEnabled() => NInputBox?.Set(TextEdit.PropertyName.VirtualKeyboardEnabled, VirtualKeyboardEnabled);

	private void PostSetVirtualKeyboardShowOnFocus() => NInputBox?.Set(TextEdit.PropertyName.VirtualKeyboardShowOnFocus, VirtualKeyboardShowOnFocus);
	#endregion // Input Box > Virtual Keyboard
}

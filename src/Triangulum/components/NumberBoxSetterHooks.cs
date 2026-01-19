using static Triangulum.Australe.Math;

using Godot;
using Triangulum.Australe.Extensions;

namespace Triangulum.Components;

internal partial class NumberBox
{
	#region Main
	private f64 ModifyNewValue(f64 value)
	{
		if (RoundingStep > 0.0D) value = Snap(value, RoundingStep);
		if (MinValueEnabled) value = f64.Max(value, MinValue);
		if (MaxValueEnabled) value = f64.Min(value, MaxValue);
		return value;
	}

	private void PostSetValue()
	{
		if (NInputBox != null)
		{
			string newText = ValueToString();

			if (NInputBox.Text != newText)
			{
				_oldInputBoxText = newText;
				NInputBox.Text = newText;
				UpdateButtons();
			}
		}
	}

	private void PostSetEnabled()
	{
		NInputBox?.Editable = Enabled;
		UpdateButtons();
	}

	private void PostSetTooltip() => NInputBox?.TooltipText = Tooltip;

	private void PostSetValueType() => PostSetValue();
	#endregion // Main
	#region Value Range
	private void PreSetMinValueEnabled(bool value)
	{
		if (value) Value = f64.Max(Value, MinValue);
	}

	private void PostSetMinValueEnabled() => UpdateButtons();

	private f64 ModifyNewMinValue(f64 value) => f64.Min(value, MaxValue);

	private void PreSetMinValue(f64 value)
	{
		if (value < Value) Value = f64.Max(Value, value);
	}

	private void PreSetMaxValueEnabled(bool value)
	{
		if (value) Value = f64.Min(Value, MaxValue);
	}

	private void PostSetMaxValueEnabled() => UpdateButtons();

	private f64 ModifyNewMaxValue(f64 value) => f64.Max(value, MinValue);

	private void PreSetMaxValue(f64 value)
	{
		if (value > Value) Value = f64.Min(Value, value);
	}
	#endregion // Value Range
	#region Value Step
	private static f64 ModifyNewIncrementStep(f64 value) => f64.Max(value, 0.0D);

	private static f64 ModifyNewDecrementStep(f64 value) => f64.Max(value, 0.0D);

	private static f64 ModifyNewRoundingStep(f64 value) => f64.Max(value, 0.0D);

	private void PreSetRoundingStep(f64 value)
	{
		if (value > 0.0D) Value = Snap(Value, value);
	}
	#endregion // Value Step
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

	private void PostSetInputBoxSelectable() => NInputBox?.SelectingEnabled = InputBoxSelectable;

	private void PostSetInputBoxDeselectOnFocusLoss() => NInputBox?.DeselectOnFocusLossEnabled = InputBoxDeselectOnFocusLoss;

	private void PostSetInputBoxAlignment() => NInputBox?.Alignment = InputBoxAlignment;
	#endregion // Input Box
	#region Input Box > Buttons
	private void PostSetIncrementButtonIcon() => NIncrementButton?.Icon = IncrementButtonIcon;

	private void PostSetDecrementButtonIcon() => NDecrementButton?.Icon = DecrementButtonIcon;
	#endregion // Input Box > Buttons
	#region Input Box > Context Menu
	private void PostSetContextMenuEnabled() => NInputBox?.ContextMenuEnabled = ContextMenuEnabled;

	private void PostSetContextMenuShortcutKeysEnabled() => NInputBox?.ShortcutKeysEnabled = ContextMenuShortcutKeysEnabled;
	#endregion // Input Box > Context Menu
	#region Input Box > Virtual Keyboard
	private void PostSetVirtualKeyboardEnabled() => NInputBox?.VirtualKeyboardEnabled = VirtualKeyboardEnabled;

	private void PostSetVirtualKeyboardShowOnFocus() => NInputBox?.VirtualKeyboardShowOnFocus = VirtualKeyboardShowOnFocus;
	#endregion // Input Box > Virtual Keyboard
}

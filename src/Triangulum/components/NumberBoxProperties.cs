using Godot;

namespace Triangulum.Components;

internal partial class NumberBox
{
	#region Main
    /// <summary>
    /// Current value.
    /// </summary>
    /// <remarks>
    /// This is not a representation of the actual current value. For that,
    /// <see cref="TrueValue"/> should be used.
    /// </remarks>
    /// <seealso cref="ValueType"/>
    /// <seealso cref="MinValue"/>
    /// <seealso cref="MaxValue"/>
    /// <seealso cref="RoundingStep"/>
    [Export]
    public f64 Value { get; set => SetProperty(ref field, value); } = 0.0D;

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
    /// Value data type.
    /// </summary>
    /// <remarks>
    /// This specifies the data type of the actual current value; it does not change
    /// <see cref="Value"/> nor related properties. The smaller the data type is, the
    /// more important it is to consider the limitations of that type.
    /// </remarks>
    [Export]
    public ValueDataType ValueType { get; set => SetProperty(ref field, value); } = ValueDataType.Float64;
    #endregion // Main
    #region Value Range
    /// <summary>
    /// If <c>false</c>, <see cref="Value"/> is never clamped to <see cref="MinValue"/>.
    /// </summary>
    [ExportGroup("Value Range"), ExportSubgroup("Minimum", "Min"), Export(PropertyHint.GroupEnable)]
    public bool MinValueEnabled { get; set => SetProperty(ref field, value); } = true;

    /// <summary>
    /// Minimum value.
    /// </summary>
    /// <remarks>
    /// <see cref="Value"/> is clamped if less than <see cref="MinValue"/>.
    /// <br/><br/>
    /// <see cref="MinValue"/> is clamped if greater than <see cref="MaxValue"/>.
    /// </remarks>
    /// <seealso cref="MinValueEnabled"/>
    [Export]
    public f64 MinValue { get; set => SetProperty(ref field, value); } = 0.0D;

    /// <summary>
    /// If <c>false</c>, <see cref="Value"/> is never clamped to <see cref="MaxValue"/>.
    /// </summary>
    [ExportSubgroup("Maximum", "Max"), Export(PropertyHint.GroupEnable)]
    public bool MaxValueEnabled { get; set => SetProperty(ref field, value); } = true;

    /// <summary>
    /// Maximum value.
    /// </summary>
    /// <remarks>
    /// <see cref="Value"/> is clamped if greater than <see cref="MaxValue"/>.
    /// <br/><br/>
    /// <see cref="MinValue"/> is clamped if less than <see cref="MinValue"/>.
    /// </remarks>
    /// <seealso cref="MaxValueEnabled"/>
    [Export]
    public f64 MaxValue { get; set => SetProperty(ref field, value); } = 100.0D;
    #endregion // Value Range
    #region Value Step
    /// <summary>
    /// Value increment step.
    /// </summary>
    /// <remarks>
    /// Minimum is <c>0.0D</c>.
    /// </remarks>
    [ExportGroup("Value Step"), Export]
    public f64 IncrementStep { get; set => SetProperty(ref field, value); } = 1.0D;

    /// <summary>
    /// Value decrement step.
    /// </summary>
    /// <remarks>
    /// Minimum is <c>0.0D</c>.
    /// </remarks>
    [Export]
    public f64 DecrementStep { get; set => SetProperty(ref field, value); } = 1.0D;

    /// <summary>
    /// If not <c>0.0D</c>, <see cref="Value"/> is rounded to a multiple of
    /// <see cref="RoundingStep"/>.
    /// </summary>
    /// <remarks>
    /// Minimum is <c>0.0D</c>.
    /// </remarks>
    [Export]
    public f64 RoundingStep { get; set => SetProperty(ref field, value); } = 0.0D;

	#if TOOLS
    [ExportToolButton("Set Inc. Step To Dec. Step")]
    private Callable IncrementStepToDecrementStepButton => new(this, nameof(IncrementStepToDecrementStep));
    private void IncrementStepToDecrementStep() => IncrementStep = DecrementStep;

    [ExportToolButton("Set Dec. Step To Inc. Step")]
    private Callable DecrementStepToIncrementStepButton => new(this, nameof(DecrementStepToIncrementStep));
    private void DecrementStepToIncrementStep() => DecrementStep = IncrementStep;
    #endif
    #endregion // Value Step
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
    #region Input Box > Buttons
    /// <summary>
    /// Icon displayed on the increment button.
    /// </summary>
    [ExportSubgroup("Buttons"), Export]
    public Texture2D? IncrementButtonIcon { get; set => SetProperty(ref field, value); } = ResourceLoader.Load<Texture2D>($"{IconsPath}/GuiSpinboxUp.svg");

    /// <summary>
    /// Icon displayed on the decrement button.
    /// </summary>
    [Export]
    public Texture2D? DecrementButtonIcon { get; set => SetProperty(ref field, value); } = ResourceLoader.Load<Texture2D>($"{IconsPath}/GuiSpinboxDown.svg");
    #endregion // Input Box > Buttons
    #region Input Box > Context Menu
    /// <summary>
    /// If <c>true</c>, right-clicking the input box opens the context menu.
    /// </summary>
    [ExportSubgroup("Context Menu", "ContextMenu"), Export]
    public bool ContextMenuEnabled { get; set => SetProperty(ref field, value); } = true;

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

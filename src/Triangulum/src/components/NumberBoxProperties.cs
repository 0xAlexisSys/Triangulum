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
    [Export]
    public f64 Value { get => _value; set => SetProperty(ref _value, value); }
    private f64 _value = 0.0D;

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
    /// Value data type.
    /// </summary>
    /// <remarks>
    /// This specifies the data type of the actual current value; it does not change
    /// <see cref="Value"/> nor related properties. The smaller the data type is, the
    /// more important it is to consider the limitations of that type.
    /// </remarks>
    [Export]
    public ValueDataType ValueType { get => _valueType; set => SetProperty(ref _valueType, value); }
    private ValueDataType _valueType = ValueDataType.Float64;
    #endregion // Main
    #region Value Range
    /// <summary>
    /// If <c>false</c>, <see cref="Value"/> is never clamped to the minimum.
    /// </summary>
    [ExportGroup(name: "Value Range", prefix: ""), ExportSubgroup(name: "Minimum", prefix: "Min"), Export(PropertyHint.GroupEnable)]
    public bool MinValueEnabled { get => _minValueEnabled; set => SetProperty(ref _minValueEnabled, value); }
    private bool _minValueEnabled = true;

    /// <summary>
    /// Minimum value.
    /// </summary>
    /// <remarks>
    /// If <see cref="Value"/> is less than this, it is clamped.
    /// </remarks>
    [Export]
    public f64 MinValue { get => _minValue; set => SetProperty(ref _minValue, value); }
    private f64 _minValue = 0.0D;

    /// <summary>
    /// If <c>false</c>, <see cref="Value"/> is never clamped to the maximum.
    /// </summary>
    [ExportSubgroup(name: "Maximum", prefix: "Max"), Export(PropertyHint.GroupEnable)]
    public bool MaxValueEnabled { get => _maxValueEnabled; set => SetProperty(ref _maxValueEnabled, value); }
    private bool _maxValueEnabled = true;

    /// <summary>
    /// Maximum value.
    /// </summary>
    /// <remarks>
    /// If <see cref="Value"/> is greater than this, it is clamped.
    /// </remarks>
    [Export]
    public f64 MaxValue { get => _maxValue; set => SetProperty(ref _maxValue, value); }
    private f64 _maxValue = 100.0D;
    #endregion // Value Range
    #region Value Step
    /// <summary>
    /// Value increment step.
    /// </summary>
    [ExportGroup(name: "Value Step", prefix: ""), Export]
    public f64 IncrementStep { get => _incrementStep; set => SetProperty(ref _incrementStep, value); }
    private f64 _incrementStep = 1.0D;

    /// <summary>
    /// Value decrement step.
    /// </summary>
    [Export]
    public f64 DecrementStep { get => _decrementStep; set => SetProperty(ref _decrementStep, value); }
    private f64 _decrementStep = 1.0D;

    /// <summary>
    /// If not <c>0.0D</c>, <see cref="Value"/> is rounded to a multiple of
    /// <see cref="RoundingStep"/>.
    /// </summary>
    [Export]
    public f64 RoundingStep { get => _roundingStep; set => SetProperty(ref _roundingStep, value); }
    private f64 _roundingStep = 0.0D;
    #endregion // Value Step
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
    #region Input Box > Buttons
    /// <summary>
    /// Icon displayed on the increment button.
    /// </summary>
    [ExportSubgroup(name: "Buttons", prefix: ""), Export]
    public Texture2D? IncrementButtonIcon { get => _incrementButtonIcon; set => SetProperty(ref _incrementButtonIcon, value); }
    private Texture2D? _incrementButtonIcon = ResourceLoader.Load<Texture2D>($"{IconsPath}/GuiSpinboxUp.svg");

    /// <summary>
    /// Icon displayed on the decrement button.
    /// </summary>
    [Export]
    public Texture2D? DecrementButtonIcon { get => _decrementButtonIcon; set => SetProperty(ref _decrementButtonIcon, value); }
    private Texture2D? _decrementButtonIcon = ResourceLoader.Load<Texture2D>($"{IconsPath}/GuiSpinboxDown.svg");
    #endregion // Input Box > Buttons
    #region Input Box > Context Menu
    /// <summary>
    /// If <c>true</c>, right-clicking the input box opens the context menu.
    /// </summary>
    [ExportSubgroup(name: "Context Menu", prefix: "ContextMenu"), Export]
    public bool ContextMenuEnabled { get => _contextMenuEnabled; set => SetProperty(ref _contextMenuEnabled, value); }
    private bool _contextMenuEnabled = true;

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
    /// If <c>true</c>, the native virtual keyboard is shown on focus events on platforms
    /// that support it.
    /// </summary>
    [Export]
    public bool VirtualKeyboardShowOnFocus { get => _virtualKeyboardShowOnFocus; set => SetProperty(ref _virtualKeyboardShowOnFocus, value); }
    private bool _virtualKeyboardShowOnFocus = true;
    #endregion // Input Box > Virtual Keyboard
}

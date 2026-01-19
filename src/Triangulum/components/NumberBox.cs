using static Triangulum.Australe.Math;

using System;
using System.Collections.Generic;
using System.Globalization;
using Godot;
using Triangulum.Classes;
using Triangulum.Australe.Extensions;

using InvalidEnumArgumentException = System.ComponentModel.InvalidEnumArgumentException;

namespace Triangulum.Components;

[GlobalClass, Icon($"{IconsPath}/SpinBox.svg"), Tool]
internal partial class NumberBox : Component
{
    public enum ValueDataType : u8
    {
        Int8,
        Int16,
        Int32,
        Int64,
        UInt8,
        UInt16,
        UInt32,
        UInt64,
        Float16,
        Float32,
        Float64,
    }

    public const i32 InputBoxSecretCharacterLength = 1;
    public const f64 Float16ReliableRange = 2048.0D;

    private static readonly Dictionary<ValueDataType, f64> s_valueDataTypeMin = new()
    {
        [ValueDataType.Int8] = i8.MinValue,
        [ValueDataType.Int16] = i16.MinValue,
        [ValueDataType.Int32] = i32.MinValue,
        [ValueDataType.Int64] = i64.MinValue,
        [ValueDataType.UInt8] = u8.MinValue,
        [ValueDataType.UInt16] = u16.MinValue,
        [ValueDataType.UInt32] = u32.MinValue,
        [ValueDataType.UInt64] = u64.MinValue,
        [ValueDataType.Float16] = (f64)f16.MinValue,
        [ValueDataType.Float32] = f32.MinValue,
        [ValueDataType.Float64] = f64.MinValue,
    };
    private static readonly Dictionary<ValueDataType, f64> s_valueDataTypeMax = new()
    {
        [ValueDataType.Int8] = i8.MaxValue,
        [ValueDataType.Int16] = i16.MaxValue,
        [ValueDataType.Int32] = i32.MaxValue,
        [ValueDataType.Int64] = i64.MaxValue,
        [ValueDataType.UInt8] = u8.MaxValue,
        [ValueDataType.UInt16] = u16.MaxValue,
        [ValueDataType.UInt32] = u32.MaxValue,
        [ValueDataType.UInt64] = u64.MaxValue,
        [ValueDataType.Float16] = (f64)f16.MaxValue,
        [ValueDataType.Float32] = f32.MaxValue,
        [ValueDataType.Float64] = f64.MaxValue,
    };
    private static readonly CultureInfo s_floatStringCulture = CultureInfo.CurrentCulture;

    public VBoxContainer? NContainer { get; private set; }
    public Label? NLabel { get; private set; }
    public HBoxContainer? NInputBoxContainer { get; private set; }
    public LineEdit? NInputBox { get; private set; }
    public VBoxContainer? NButtonContainer { get; private set; }
    public Button? NIncrementButton { get; private set; }
    public Button? NDecrementButton { get; private set; }
    public object TrueValue => ValueType switch
    {
        ValueDataType.Int8 => (i8)Value,
        ValueDataType.Int16 => (i16)Value,
        ValueDataType.Int32 => (i32)Value,
        ValueDataType.Int64 => (i64)Value,
        ValueDataType.UInt8 => (u8)Value,
        ValueDataType.UInt16 => (u16)Value,
        ValueDataType.UInt32 => (u32)Value,
        ValueDataType.UInt64 => (u64)Value,
        ValueDataType.Float16 => (f16)Value,
        ValueDataType.Float32 => (f32)Value,
        ValueDataType.Float64 => Value,
        _ => throw new InvalidEnumArgumentException(nameof(ValueType)),
    };

    private string _oldInputBoxText = string.Empty;

    public NumberBox() : base()
    {
        #if TOOLS
        if (IsInGroup(GroupName.Components)) return;
        #endif

        _oldInputBoxText = ValueToString();

        NContainer = new();
        SetNodeIdentifier(NContainer, "Container");
        AddInternalChild(NContainer, this);

        NLabel = new()
        {
            Visible = !LabelText.IsEmpty(),
        };
        SetNodeIdentifier(NLabel, "Label");
        AddInternalChild(NLabel, NContainer);

        NInputBoxContainer = new()
        {
            SizeFlagsVertical = SizeFlags.ExpandFill,
        };
        SetNodeIdentifier(NInputBoxContainer, "InputBoxContainer");
        AddInternalChild(NInputBoxContainer, NContainer);

        NInputBox = new()
        {
            Text = _oldInputBoxText,
            KeepEditingOnTextSubmit = true,
            VirtualKeyboardType = LineEdit.VirtualKeyboardTypeEnum.NumberDecimal,
            CustomMinimumSize = ComponentInputMinSize,
            SizeFlagsHorizontal = SizeFlags.ExpandFill,
        };
        SetNodeIdentifier(NInputBox, "InputBox");
        NInputBox.TextSubmitted += newText =>
        {
            if (TryEvaluateNumberExpression(newText, out NumberExprResult result))
            {
                #if NET7_0_OR_GREATER && NUMBER_EXPR_TO_DECIMAL
                Value = (f64)result;
                #else
                Value = result;
                #endif
            }
            else
            {
                NInputBox.Text = _oldInputBoxText;
            }
        };
        AddInternalChild(NInputBox, NInputBoxContainer);

        NButtonContainer = new();
        SetNodeIdentifier(NButtonContainer, "ButtonContainer");
        AddInternalChild(NButtonContainer, NInputBoxContainer);

        NIncrementButton = new()
        {
            Icon = IncrementButtonIcon,
            IconAlignment = HorizontalAlignment.Center,
            SizeFlagsVertical = SizeFlags.ExpandFill,
        };
        SetNodeIdentifier(NIncrementButton, "IncrementButton");
        NIncrementButton.Pressed += () => Value += IncrementStep;
        AddInternalChild(NIncrementButton, NButtonContainer);

        NDecrementButton = new()
        {
            Icon = DecrementButtonIcon,
            IconAlignment = HorizontalAlignment.Center,
            SizeFlagsVertical = SizeFlags.ExpandFill,
        };
        SetNodeIdentifier(NDecrementButton, "DecrementButton");
        NDecrementButton.Pressed += () => Value -= DecrementStep;
        AddInternalChild(NDecrementButton, NButtonContainer);

        #if TOOLS
        ConfigurationWarningMethods.Add(() => IsUsingIntType() && Value.HasDecimals() ? $"{nameof(Value)} has decimals but {nameof(ValueType)} is {ValueType}" : string.Empty);

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        ConfigurationWarningMethods.Add(() => MinValue == MaxValue ? $"{nameof(MinValue)} is equal to {nameof(MaxValue)}" : string.Empty);

        ConfigurationWarningMethods.Add(() =>
        {
            if (!MinValueEnabled) return string.Empty;

            f64 valueDataTypeMin = s_valueDataTypeMin[ValueType];
            return MinValue < valueDataTypeMin ? $"{nameof(MinValue)} is less than {ValueType}'s minimum value ({valueDataTypeMin})" : string.Empty;
        });
        ConfigurationWarningMethods.Add(() => IsUsingIntType() && MinValueEnabled && MinValue.HasDecimals() ? $"{nameof(MinValue)} has decimals but {nameof(ValueType)} is {ValueType}" : string.Empty);
        ConfigurationWarningMethods.Add(() =>
        {
            if (ValueType != ValueDataType.Float16 || !MinValueEnabled) return string.Empty;
            return MinValue < -Float16ReliableRange || MinValue > Float16ReliableRange
                ? $"{nameof(MinValue)} is outside {ValueDataType.Float16}'s reliable range (approximately -{Float16ReliableRange} to {Float16ReliableRange})"
                : string.Empty;
        });

        ConfigurationWarningMethods.Add(() =>
        {
            if (!MaxValueEnabled) return string.Empty;

            f64 valueDataTypeMax = s_valueDataTypeMax[ValueType];
            return MaxValue > valueDataTypeMax ? $"{nameof(MaxValue)} is greater than {ValueType}'s maximum value ({valueDataTypeMax})" : string.Empty;
        });
        ConfigurationWarningMethods.Add(() => IsUsingIntType() && MaxValueEnabled && MaxValue.HasDecimals() ? $"{nameof(MaxValue)} has decimals but {nameof(ValueType)} is {ValueType}" : string.Empty);
        ConfigurationWarningMethods.Add(() =>
        {
            if (ValueType != ValueDataType.Float16 || !MaxValueEnabled) return string.Empty;
            return MaxValue < -Float16ReliableRange || MaxValue > Float16ReliableRange
                ? $"{nameof(MaxValue)} is outside {ValueDataType.Float16}'s reliable range (approximately -{Float16ReliableRange} to {Float16ReliableRange})"
                : string.Empty;
        });

        ConfigurationWarningMethods.Add(() => IsUsingIntType() && IncrementStep.HasDecimals() ? $"{nameof(IncrementStep)} has decimals but ValueType is {ValueType}" : string.Empty);
        ConfigurationWarningMethods.Add(() => IsUsingIntType() && DecrementStep.HasDecimals() ? $"{nameof(DecrementStep)} has decimals but ValueType is {ValueType}" : string.Empty);
        ConfigurationWarningMethods.Add(() => IsUsingIntType() && RoundingStep.HasDecimals() ? $"{nameof(RoundingStep)} has decimals but ValueType is {ValueType}" : string.Empty);

        ConfigurationWarningMethods.Add(() => IncrementButtonIcon == null ? $"{nameof(IncrementButtonIcon)} is not set" : string.Empty);
        ConfigurationWarningMethods.Add(() => DecrementButtonIcon == null ? $"{nameof(DecrementButtonIcon)} is not set" : string.Empty);
        return;

        bool IsUsingIntType() => ValueType switch
        {
            ValueDataType.Int8 or ValueDataType.Int16 or ValueDataType.Int32 or ValueDataType.Int64 or
            ValueDataType.UInt8 or ValueDataType.UInt16 or ValueDataType.UInt32 or ValueDataType.UInt64 => true,
            ValueDataType.Float16 or ValueDataType.Float32 or ValueDataType.Float64 => false,
            _ => throw new InvalidEnumArgumentException(nameof(ValueType)),
        };
        #endif
    }

    private string ValueToString() => ValueType switch
    {
        ValueDataType.Int8 or ValueDataType.Int16 or ValueDataType.Int32 or ValueDataType.Int64 or
        ValueDataType.UInt8 or ValueDataType.UInt16 or ValueDataType.UInt32 or ValueDataType.UInt64 => TrueValue.ToString()!,
        ValueDataType.Float16 => ((f16)Value).ToString("G4", s_floatStringCulture),
        ValueDataType.Float32 => ((f32)Value).ToString("G7", s_floatStringCulture),
        ValueDataType.Float64 => Value.ToString(s_floatStringCulture), // No formatting needed for f64.
        _ => throw new InvalidEnumArgumentException(nameof(ValueType)),
    };

    private void UpdateButtons()
    {
        f64 trueValue = ValueType != ValueDataType.Float16 ? Convert.ToDouble(TrueValue) : (f64)(f16)TrueValue;

        NIncrementButton?.Set(BaseButton.PropertyName.Disabled, !Enabled || MaxValueEnabled && trueValue >= MaxValue);
        NDecrementButton?.Set(BaseButton.PropertyName.Disabled, !Enabled || MinValueEnabled && trueValue <= MinValue);
    }
}

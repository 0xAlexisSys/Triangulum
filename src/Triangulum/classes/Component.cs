using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;
using Triangulum.Australe.Extensions;

namespace Triangulum.Classes;

/// <summary>
/// Base class for all dynamic UI components.
/// </summary>
[GlobalClass]
internal abstract partial class Component : PanelContainer
{
	/// <summary>
	/// Called when a property changes.
	/// </summary>
	public event EventHandler<string, object>? PropertyChanged;

	private enum SetterHookMethodBehavior : u8
	{
		Default,
		UseValue,
		ModifyValue,
	}

	private const BindingFlags SetterHookMethodBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;
	private const BindingFlags PropertyBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

	#if TOOLS
	protected readonly List<Func<string>> ConfigurationWarningMethods = [];
	#endif

	private List<Action>? _deferredSetterMethods = [];

	private readonly Type _cachedType;
	private readonly ConcurrentDictionary<string, MethodInfo?> _setterHookMethodCache = [];

	protected Component()
	{
		_cachedType = GetType();
        ThemeTypeVariation = _cachedType.Name;
	}

	public override void _Ready()
    {
		if (_deferredSetterMethods != null)
		{
			foreach (Action method in _deferredSetterMethods) method();
			_deferredSetterMethods = null;
		}

		AddToGroup(GroupName.Components);
    }

	#if TOOLS
	public override string[] _GetConfigurationWarnings()
	{
		// List allows configuration warnings to be added dynamically.
		// The variable is converted to Array when returned.
        List<string> warnings = [];
		warnings.AddRange(ConfigurationWarningMethods.Select(method => method().Trim()).Where(warning => !warning.IsEmpty()));
		return [.. warnings];
	}
	#endif

	private protected void SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
	{
		if (!IsNodeReady()) // Node is not yet ready, delay the setter method to prevent potential bugs.
		{
			// Get the compiler-generated backing field.
			FieldInfo backingField = _cachedType.GetField($"<{propertyName}>k__BackingField", PropertyBindingFlags)!;

			// As we want the new value to be visible, the backing field is set beforehand.
			backingField.SetValue(this, value);

			_deferredSetterMethods?.Add(() =>
			{
				T property = (T)backingField.GetValue(this)!;
				SetPropertyInternal(ref property);
			});
		}
		else // Node is ready, call the setter method immediately.
		{
			SetPropertyInternal(ref property);
		}
		return;

		// ReSharper disable once RedundantAssignment
		void SetPropertyInternal(ref T property)
		{
			CallSetterHookMethod("ModifyNew", SetterHookMethodBehavior.ModifyValue);
			CallSetterHookMethod("PreSet", SetterHookMethodBehavior.UseValue);
			property = value;
			CallSetterHookMethod("PostSet", SetterHookMethodBehavior.Default);
			PropertyChanged?.Invoke(propertyName, value!);
			#if TOOLS
			UpdateConfigurationWarnings();
			#endif
			return;

			void CallSetterHookMethod(string name, SetterHookMethodBehavior behavior)
			{
				string methodName = $"{name}{propertyName}";
				if (!_setterHookMethodCache.TryGetValue(methodName, out MethodInfo? method))
				{
					method = _cachedType.GetMethod(methodName, SetterHookMethodBindingFlags);
					_setterHookMethodCache[methodName] = method;
				}

				if (method != null)
				{
					switch (behavior)
					{
						default:
						case SetterHookMethodBehavior.Default:
							method.Invoke(this, null);
							break;
						case SetterHookMethodBehavior.UseValue:
							method.Invoke(this, [value]);
							break;
						case SetterHookMethodBehavior.ModifyValue:
							value = (T)method.Invoke(this, [value])!;
							break;
					}
				}
			}
		}
	}

	private protected void SetNodeIdentifier(Node node, string identifier)
	{
		(node as Control)?.ThemeTypeVariation = $"{_cachedType.Name}_{identifier}";
		node.Name = identifier;
	}

	private protected void AddInternalChild(Node node, Node parentNode)
	{
		parentNode.AddChild(node, @internal: InternalMode.Front);
		node.Owner = this;
	}
}

# AGENTS.md

This is a set of guidelines for AI code agents to follom, to help them understand the codebase better.

## Project Overview

Triangulum is a general-purpose dynamic UI framework built with Godot Engine, leveraging C# for greater versatility over Godot's in-house GDScript. It is inspired by the Python libraries Gradio and Streamlit, enabling developers to easily build dynamic apps.

**Tech Stack:**

- `Godot 4.5.1`
- `.NET 10.0`
- `C# 14.0`

**File Structure:**

- `src/`
  - `GlobalNamespaces.cs` - Universal namespaces.
  - `GlobalAliases.cs` - Universal aliases.
  - `Triangulum/`
  - `Triangulum.Australe/`
- `docs/`

## Project Details

### Triangulum

Godot namespaces are available. Assume that the C# API mirrors most of the GDScript API and keep the following in mind:

- What is found in GDScript usually follows C# code conventions (**PascalCase**, naming, etc).
- Math functions such as `snapped()` and `wrap()`  are not provided. Use C# math functions instead.
- C# collections should be used over Godot collections unless the code needs to interface with the engine.
  - **Example:** `[Export] public GDStringArray MyStringArray = [];`

### Triangulum.Australe

Prefer writing independent code; Godot namespaces are not available. This does not mean avoiding NuGet package namespaces, if any, but the code should be able to work without Godot in the equation.

## Code Style

The following is an example of a general class making use of the overall code style:

```csharp
using System;
using System.Numerics;

namespace Triangulum.Australe;

/// <summary>
/// Base class for all objects.
/// </summary>
public abstract class WorldObject(string name)
{
    public const f32 DefaultGravity = 9.81F;

    public static u64 TotalObjectsCreated = 0UL;

    public string Name = name;
    public Vector3 Position = Vector3.Zero;
    public f32 Mass = 1.0F;
    public Vector3 Velocity = Vector3.Zero;

    protected bool IsActive = true;
    protected f64 LastUpdateTime = 0.0D;
    protected readonly u64 ObjectID = TotalObjectsCreated++;

    private readonly i64 _creationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    public virtual void Update(f32 deltaTime)
    {
        if (!IsActive) return;

        f32 gravity = -DefaultGravity * deltaTime;
        Velocity.Y += gravity;

        Position += Velocity * deltaTime;
        LastUpdateTime = deltaTime;
    }

    public f64 GetDistanceTo(WorldObject other)
    {
        f64 dx = other.Position.X - Position.X;
        f64 dy = other.Position.Y - Position.Y;
        f64 dz = other.Position.Z - Position.Z;

        return Sqrt(dx * dx + dy * dy + dz * dz);
    }

    protected void ApplyForce(Vector3 force, f32 duration)
    {
        f32 acceleration = force.Length() / Mass;
        Vector3 deltaVelocity = Vector3.Normalize(force) * acceleration * duration;

        Velocity += deltaVelocity;
    }

    protected i32 CalculateComplexity()
    {
        i32 complexity = 1;

        if (Mass > 10.0F) complexity += 2;
        if (Velocity.Length() > 5.0F) complexity += 3;

        return complexity;
    }
}

public sealed class PhysicsObject : WorldObject
{
    public static readonly f32 MinMass = 0.1F;
    public static readonly f32 MaxMass = 1000.0F;

    private readonly u64 _physicsLayerMask = 0xFFFFFFFFFFFFFFFFUL;

    public PhysicsObject(string name, f32 mass) : base(name)
    {
        SetMass(mass);
    }

    private void SetMass(f32 value)
    {
        Mass = f32.Clamp(value, MinMass, MaxMass);
    }
}
```

### Conventions

Prefer using literals (if possible) and type aliases for numbers. Note that the data types `Int128`, `UInt128`, and `Half` do not have literals, so they use whatever makes sense logically; `Half` variables require an explicit cast as well.

```csharp
i8 mySByte = 8;
i16 myShort = 16;
i32 myInt = 32;
i64 myLong = 64L;
i128 myExtraLong = 128L;

u8 myByte = 8;
u16 myUShort = 16;
u32 myUInt = 32U;
u64 myULong = 64UL;
u128 myUExtraLong = 128UL;

f16 myHalf = (f16)16.0F;
f32 mySingle = 32.0F;
f64 myDouble = 64.0D;
f128 myDecimal = 128.0M;

iLG myBigInteger = iLG.Zero;
```

If the number is a compile-time type alias, use an explicit cast and omit the literal.

```
#if !GODOT_REAL_T_IS_DOUBLE
global using RealT = System.Single;
#else
global using RealT = System.Double;
#endif

GDVector2 position = new((RealT)64.0, (RealT)128.0);
```

---

Code should strive to be self-explanatory and use comments wherever necessary. For example, if a piece of code may be unclear at a first glance, its function should be described.

```csharp
public static void FreeChildren(this Node parentNode, bool includeInternal = false)
{
    // ⛔ BAD COMMENT:
    // Get all children and internal children, and free them.
    foreach (Node node in parentNode.GetChildren(includeInternal))
    {
        node.Free();
    }
}

public static u64 MaxOfUInt(u8 bits)
{
    if (bits == 0 || bits > 64)
    {
        throw new ArgumentOutOfRangeException(nameof(bits));
    }

    // ✅ GOOD COMMENT:
    // If using 64 bits, bit shift operations will cause the returned value to overflow,
    // so return the maximum value of a 64-bit unsigned integer instead.
    return bits != 64 ? (1UL << bits) - 1UL : u64.MaxValue;
}
```

### Organization

**Top-Level:**

```csharp
using static System.MathF;
using static System.Math;

using System;

using MyAlias = System.Collections.Generic.List<string>;

namespace MyNamespace;

internal class MyClass;
```

---

Members are sorted by access modifiers:

1. `public`
2. `protected`
3. `private`
4. `private protected`
5. `internal`
6. `protected internal`

A member's position is also determined by the presence of specific keywords:

```csharp
protected enum MyEnum : u8
{
    Llama,
    Mistral,
    Gemma,
    Qwen,
}

private protected const f32 MyConstFloat32 = 1000.0F;

public static string MyStaticString = "";
public static readonly bool MyStaticReadOnlyBool = true;

private i64 _myLong = 64L;
private protected readonly u64 _myReadOnlyULong = 64UL;
```

### Documentation Style

More to come in the future...

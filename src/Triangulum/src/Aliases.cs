// ReSharper disable RedundantUsingDirective.Global
#pragma warning disable IDE0005
#pragma warning disable IDE0049

#if !GODOT_REAL_T_IS_DOUBLE
global using RealT = System.Single;
#else
global using RealT = System.Double;
#endif

global using GDVector2 = Godot.Vector2;
global using GDVector2I = Godot.Vector2I;
global using GDVector3 = Godot.Vector3;
global using GDVector3I = Godot.Vector3I;
global using GDVector4 = Godot.Vector4;
global using GDVector4I = Godot.Vector4I;

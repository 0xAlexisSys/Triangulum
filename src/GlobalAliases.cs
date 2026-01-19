// ReSharper disable RedundantUsingDirective.Global
#pragma warning disable IDE0079
#pragma warning disable IDE0005
#pragma warning disable IDE0049

global using i8 = System.SByte;
global using i16 = System.Int16;
global using i32 = System.Int32;
global using i64 = System.Int64;
#if NET7_0_OR_GREATER
global using i128 = System.Int128;
#endif

global using u8 = System.Byte;
global using u16 = System.UInt16;
global using u32 = System.UInt32;
global using u64 = System.UInt64;
#if NET7_0_OR_GREATER
global using u128 = System.UInt128;
#endif

#if NET5_0_OR_GREATER
global using f16 = System.Half;
#endif
global using f32 = System.Single;
global using f64 = System.Double;
global using f128 = System.Decimal;

global using iLG = System.Numerics.BigInteger;

global using Vector2 = System.Numerics.Vector2;
global using Vector3 = System.Numerics.Vector3;
global using Vector4 = System.Numerics.Vector4;

#if NET7_0_OR_GREATER && NUMBER_EXPR_TO_DECIMAL
global using NumberExprResult = System.Decimal;
#else
global using NumberExprResult = System.Double;
#endif

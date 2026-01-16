using Godot;

namespace Triangulum.Extensions;

internal static partial class GDExtensionMembers
{
    public static void QueueFreeChildren(this Node parentNode, bool includeInternal = false)
    {
        foreach (Node node in parentNode.GetChildren(includeInternal))
        {
            node.QueueFree();
        }
    }

    public static void FreeChildren(this Node parentNode, bool includeInternal = false)
    {
        foreach (Node node in parentNode.GetChildren(includeInternal))
        {
            node.Free();
        }
    }
}

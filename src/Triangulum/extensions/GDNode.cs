using Godot;

namespace Triangulum.Extensions;

internal static partial class GDExtensionMembers
{
    extension(Node @this)
    {
	    public void QueueFreeChildren(bool includeInternal = false)
	    {
		    foreach (Node node in @this.GetChildren(includeInternal))
		    {
			    node.QueueFree();
		    }
	    }

	    public void FreeChildren(bool includeInternal = false)
	    {
		    foreach (Node node in @this.GetChildren(includeInternal))
		    {
			    node.Free();
		    }
	    }
    }
}

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventBus = SCALE.Events.EventBus;

namespace SCALE;

public static class Extensions
{
	public static bool IsValid([NotNullWhen(true)] this GodotObject? inst)
	{
		return GodotObject.IsInstanceValid(inst);
	}
    
    public static string Reverse(this string text)
    {
        var sb = new StringBuilder(text.Length);
        for (int i = text.Length - 1; i >= 0; i--)
        {
            sb.Append(text[i]);
        }

        return sb.ToString();
    }
	
	public static bool IsNotValid([NotNullWhen(false)] this GodotObject? inst)
	{
		return !GodotObject.IsInstanceValid(inst);
	}
	
	public static Action<T> Debounce<T>(this Action<T> func, int milliseconds = 300)
	{
		var last = 0;
		return arg =>
		{
			var current = Interlocked.Increment(ref last);
			Task.Delay(milliseconds).ContinueWith(task =>
			{
				if (current == last) func(arg);
				task.Dispose();
			});
		};
	}
	
	public static EventBus GetEventBus(this Node node)
	{
		return node.GetNode<EventBus>(EventBus.NodePath);
	}

	public static T? GetFirstChildByType<[MustBeVariant]T>(this Node node, bool internalMode = false)
		where T : class
	{
		var children = node.GetChildren(internalMode);

		foreach (var child in children)
		{
			if (child is T c)
			{
				return c;
			}
		}

		return null;
	}
	
	public static int IndexOfMin(this List<float> self)
	{
		if (self == null)
		{
			throw new ArgumentNullException(nameof(self));
		}

		if (self.Count == 0)
		{
			throw new ArgumentException("List is empty.", nameof(self));
		}

		var min = self[0];
		var minIndex = 0;

		for (var i = 1; i < self.Count; ++i)
		{
			if (self[i] < min)
			{
				min = self[i];
				minIndex = i;
			}
		}

		return minIndex;
	}
}

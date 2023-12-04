using System.Diagnostics.CodeAnalysis;

namespace HetznerServerNotifier;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class ValueEqualityList<T> : List<T>
{
	public override bool Equals(object? obj) => obj is IEnumerable<T> enumerable && enumerable.SequenceEqual(this);

	public override int GetHashCode()
	{
		return this.Aggregate(0, (current, item) => current ^ item!.GetHashCode());
	}
}

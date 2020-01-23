using UnityEngine;

namespace Waker
{
	public interface IPool<T>
	{
		T Original { get; }

		T One(bool activate = true);

		T One(Vector3 position, Quaternion rotation, bool activate = true);
	}
}

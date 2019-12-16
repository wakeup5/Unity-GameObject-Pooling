using UnityEngine;

namespace Waker
{
	public interface IPool<T>
	{
		T Original { get; }

		T ActivateOne(System.Action<T> beforeActivation = null);

		T ActivateOne(Vector3 position, Quaternion rotation, System.Action<T> beforeActivation = null);
	}
}

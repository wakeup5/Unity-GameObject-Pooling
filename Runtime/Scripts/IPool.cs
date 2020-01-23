using UnityEngine;

namespace Waker
{
	public interface IPool<T> : System.IDisposable
	{
		T Original { get; }

		T One(bool activate = true);

		T One(Vector3 position, Quaternion rotation, bool activate = true);

		//void Destroy();
	}
}

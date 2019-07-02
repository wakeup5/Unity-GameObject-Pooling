using UnityEngine;

namespace MountainOutside
{
	public interface IPool<T> {

		T Original { get; }

		T ActivateOne();

		T ActivateOne(Vector3 position, Quaternion rotation);
	}
}
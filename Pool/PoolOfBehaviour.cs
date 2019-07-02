using System.Collections.Generic;
using UnityEngine;

namespace MountainOutside
{
	public partial class PoolOfBehaviour<T> : IPool<T> where T : MonoBehaviour
	{
		private Transform parent = null;
		private List<T> pool = null;

		public T Original { get; }

		public PoolOfBehaviour(T original, int capacity = 5, Transform parant = null)
		{
			this.Original = original ?? throw new System.ArgumentNullException("In pooling the original can not be null.");
			this.Original.gameObject.SetActive(false);
			this.parent = parant ?? Pool.FindOrCreateParent(original.name);
			this.pool = new List<T>();

			for (int i = 0; i < capacity; i++)
			{
				this.pool.Add(Object.Instantiate<T>(this.Original, this.parent));
			}
		}

		public T ActivateOne()
		{
			T result = null;
			foreach (T obj in pool)
			{
				if (!obj.gameObject.activeSelf)
				{
					result = obj;
					break;
				}
			}
			if (result == null)
			{
				result = Object.Instantiate<T>(this.Original, this.parent);
				pool.Add(result);
			}
			result.gameObject.SetActive(true);
			return result;
		}

		public T ActivateOne(Vector3 position, Quaternion rotation)
		{
			T result = ActivateOne();
			result.transform.SetPositionAndRotation(position, rotation);
			return result;
		}
	}
}
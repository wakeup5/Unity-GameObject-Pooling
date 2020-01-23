using System.Collections.Generic;
using UnityEngine;

namespace Waker
{
	public partial class PoolOfBehaviour<T> : IPool<T> where T : MonoBehaviour
	{
		private Transform parent = null;
		private List<T> pool = null;

		public T Original { get; }

		public PoolOfBehaviour(T original, int capacity = 5, Transform parent = null)
		{
			this.Original = original ?? throw new System.ArgumentNullException("In pooling the original can not be null.");
			this.Original.gameObject.SetActive(false);
			this.parent = parent ?? Pool.FindOrCreateParent(original.name);
			this.pool = new List<T>();

			for (int i = 0; i < capacity; i++)
			{
				this.pool.Add(Object.Instantiate<T>(this.Original, this.parent));
			}

			var e =
				this.parent.GetComponent<DestroyPoolOfLifeCycle>() ??
				this.parent.gameObject.AddComponent<DestroyPoolOfLifeCycle>();

			e.Regist(original.GetInstanceID());
		}

		public T One(bool activate = true)
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

			if (activate)
			{
				result.gameObject.SetActive(true);
			}

			return result;
		}

		public T One(Vector3 position, Quaternion rotation, bool activate = true)
		{
			T result = One(false);
			result.transform.SetPositionAndRotation(position, rotation);

			if (activate)
			{
				result.gameObject.SetActive(true);
			}

			return result;
		}

		public void Dispose()
		{
			Object.Destroy(parent);
		}
	}
}

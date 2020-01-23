using System.Collections.Generic;
using UnityEngine;

namespace Waker
{
	// pool of Poolable
	public class PoolOfPoolable<T> : IPool<T> where T : Poolable<T>
	{
		private Transform parent = null;
		private Queue<T> container = null;

		public T Original { get; }

		public PoolOfPoolable(T original, int capacity = 5)
		{
			this.Original = original ?? throw new System.ArgumentNullException("In pooling the original can not be null.");
			this.Original.gameObject.SetActive(false);
			this.parent = Pool.FindOrCreateParent(original.name);
			this.container = new Queue<T>();

			for (int i = 0; i < capacity; i++)
			{
				var instance = Object.Instantiate<T>(this.Original, this.parent);
				instance.Pool = this;

				this.container.Enqueue(instance);
			}

			var e =
				this.parent.GetComponent<DestroyPoolOfLifeCycle>() ??
				this.parent.gameObject.AddComponent<DestroyPoolOfLifeCycle>();

			e.Regist(original.GetInstanceID());
		}

		public T One(bool activate = true)
		{
			T result = null;

			if (container.Count == 0)
			{
				result = Object.Instantiate<T>(this.Original, this.parent);
				result.Pool = this;
			}
			else
			{
				result = container.Dequeue();
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

		public void Withdraw(T poolable)
		{
			container.Enqueue(poolable);
		}

		public void Dispose()
		{
			Object.Destroy(parent);
		}
	}
}

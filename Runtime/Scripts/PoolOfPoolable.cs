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
			this.parent = parent ?? FindOrCreateParent(original.name);
			this.container = new Queue<T>();

			for (int i = 0; i < capacity; i++)
			{
				var instance = Object.Instantiate<T>(this.Original, this.parent);
				instance.Pool = this;

				this.container.Enqueue(instance);
			}
		}

		public T ActivateOne()
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

			result.gameObject.SetActive(true);
			return result;
		}

		public T ActivateOne(Vector3 position, Quaternion rotation)
		{
			T result = ActivateOne();
			result.transform.SetPositionAndRotation(position, rotation);
			return result;
		}

		public void Withdraw(T poolable)
		{
			container.Enqueue(poolable);
		}

		private static Transform FindOrCreateParent(string originalName)
		{
			if (Pool.poolRoot == null)
			{
				Pool.poolRoot = new GameObject("Pools").transform;
			}

			string name = string.Format("Pool-{0}", originalName);
			Transform p = Pool.poolRoot.Find(name);

			if (p == null)
			{
				p = new GameObject(name).transform;
				p.parent = Pool.poolRoot;
			}

			return p;
		}
	}
}
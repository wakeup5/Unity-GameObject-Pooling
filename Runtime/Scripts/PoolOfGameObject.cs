using System.Collections.Generic;
using UnityEngine;

namespace Waker
{
	// Pool of GameObject
	public partial class PoolOfGameObject : IPool<GameObject>
	{
		private Transform parent = null;
		private List<GameObject> pool = null;

		public GameObject Original { get; }

		public PoolOfGameObject(GameObject original, int capacity = 5)
		{
			this.Original = original ?? throw new System.ArgumentNullException("In pooling the original can not be null.");
			this.Original.SetActive(false);
			this.parent = Pool.FindOrCreateParent(original.name);
			this.pool = new List<GameObject>();

			for (int i = 0; i < capacity; i++)
			{
				this.pool.Add(Object.Instantiate<GameObject>(this.Original, this.parent));
			}
		}

		public GameObject ActivateOne(System.Action<GameObject> beforeActivation = null)
		{
			GameObject result = null;
			foreach (GameObject obj in pool)
			{
				if (!obj.activeSelf)
				{
					result = obj;
					break;
				}
			}

			if (result == null)
			{
				result = Object.Instantiate<GameObject>(this.Original, this.parent);
				pool.Add(result);
			}

			beforeActivation?.Invoke(result);

			result.SetActive(true);
			return result;
		}

		public GameObject ActivateOne(Vector3 position, Quaternion rotation, System.Action<GameObject> beforeActivation = null)
		{
			GameObject result = ActivateOne();
			result.transform.SetPositionAndRotation(position, rotation);
			return result;
		}
	}
}

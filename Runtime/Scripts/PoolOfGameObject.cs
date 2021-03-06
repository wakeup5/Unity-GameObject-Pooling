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

		public PoolOfGameObject(GameObject original, int capacity = 5, Transform parent = null)
		{
			this.Original = original ?? throw new System.ArgumentNullException("In pooling the original can not be null.");
			this.Original.SetActive(false);
			this.parent = parent ?? Pool.FindOrCreateParent(original.name);
			this.pool = new List<GameObject>();

			for (int i = 0; i < capacity; i++)
			{
				this.pool.Add(Object.Instantiate<GameObject>(this.Original, this.parent));
			}

			var e =
				this.parent.GetComponent<DestroyPoolOfLifeCycle>() ??
				this.parent.gameObject.AddComponent<DestroyPoolOfLifeCycle>();

			e.Regist(original.GetInstanceID());
		}

		public GameObject One(bool activate = true)
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

			if (activate)
			{
				result.SetActive(true);
			}

			return result;
		}

		public GameObject One(Vector3 position, Quaternion rotation, bool activate = true)
		{
			GameObject result = One(false);
			result.transform.SetPositionAndRotation(position, rotation);

			if (activate)
			{
				result.SetActive(true);
			}

			return result;
		}

		public void Dispose()
		{
			Object.Destroy(parent);
		}
	}
}

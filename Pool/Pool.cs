using System.Collections.Generic;
using UnityEngine;

namespace MountainOutside
{
	public static class Pool
	{
		internal static Transform poolRoot;

		private static Dictionary<int, object> pools = new Dictionary<int, object>();

		public static IPool<T> OfPoolable<T>(T original, int capacity = 5) where T : Poolable<T>
		{
			if (original == null)
			{
				return null;
			}

			int id = original.GetInstanceID();

			if (pools.TryGetValue(id, out object result))
			{
				return result as IPool<T>;
			}

			IPool<T> n = new PoolOfPoolable<T>((T)original, capacity);
			pools.Add(id, n);
			return n;
		}

		// for MonoBehaviour or UI object
		public static IPool<T> OfBehaviour<T>(T original, int capacity = 5, Transform parents = null) where T : MonoBehaviour
		{
			if (original == null)
			{
				return null;
			}

			int id = original.GetInstanceID();

			if (pools.TryGetValue(id, out object result))
			{
				return result as IPool<T>;
			}

			IPool<T> n = new PoolOfBehaviour<T>(original, capacity, parents);
			pools.Add(id, n);
			return n;
		}

		public static IPool<GameObject> OfGameObject(GameObject original, int capacity = 5)
		{
			if (original == null)
			{
				return null;
			}

			int id = original.GetInstanceID();

			if (pools.TryGetValue(id, out object result))
			{
				return result as IPool<GameObject>;
			}

			IPool<GameObject> n = new PoolOfGameObject(original, capacity);
			pools.Add(id, n);
			return n;
		}

		public static Transform FindOrCreateParent(string originalName)
		{
			if (poolRoot == null)
			{
				poolRoot = new GameObject("Pools").transform;
			}

			string name = string.Format("Pool-{0}", originalName);
			Transform p = poolRoot.Find(name);

			if (p == null)
			{
				p = new GameObject(name).transform;
				p.parent = poolRoot;
			}

			return p;
		}
	}
}
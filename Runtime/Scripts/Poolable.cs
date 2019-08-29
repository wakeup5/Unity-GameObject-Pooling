using MountainOutside;
using UnityEngine;

namespace MountainOutside
{
	public interface IPoolable 
	{

	}
	public class Poolable : Poolable<Poolable>
	{
	
	}
	public class Poolable<T> : MonoBehaviour, IPoolable where T : Poolable<T>
	{
		private PoolOfPoolable<T> pool;
		public T Original { get { return Pool?.Original; } }
		public IPool<T> Pool 
		{ 
			get { return pool; } 
			internal set { pool = value as PoolOfPoolable<T>; }
		}
		protected virtual void OnDisable()
		{
			pool.Withdraw((T)this);
		}
	}
}

using UnityEngine;

namespace Waker
{
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
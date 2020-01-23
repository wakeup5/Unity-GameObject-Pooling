using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waker
{
	public class DestroyPoolOfLifeCycle : MonoBehaviour
	{
		private int id;

		private void OnDestroy()
		{
			Pool.DestroyPool(id);
		}

		public void Regist(int id)
		{
			this.id = id;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
	public class DestroyAfter : MonoBehaviour
	{
		public float time_alive;

		public void Update()
		{
			time_alive -= Time.deltaTime;

			if (time_alive <= 0.0)
			{
				Destroy(this.gameObject);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Vehicles;
using UnityEngine;

namespace Assets.Projectiles
{
	public class Bullet : Projectile
	{
		public GameObject hitEffect;

		public override float damage => 10;
        public override bool hostile => false;

        public const float speed = 10.0f;

		float currentLifetime = 0;

		public void Update()
		{
			currentLifetime += Time.deltaTime;

			if (currentLifetime >= 2)
			{
				Destroy(this.gameObject);
			}

			this.transform.position += transform.forward * speed * Time.deltaTime;
		}

		public override bool Hit(Vehicle vehicle)
		{
			Instantiate(hitEffect, this.transform.position, this.transform.rotation);

			return true;
		}
	}
}

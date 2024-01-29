using Assets.Vehicles;
using UnityEngine;

namespace Assets.Projectiles
{
	public class LaserHitDetector : Projectile
	{
		public GameObject hitEffect;

		public override float damage => 100;
        public override bool hostile => true;

        public override bool Hit(Vehicle vehicle)
		{
			Instantiate(hitEffect, vehicle.transform.position, vehicle.transform.rotation);

			return true;
		}
	}
}

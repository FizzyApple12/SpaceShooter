using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Vehicles;
using UnityEngine;

namespace Assets.Projectiles
{
	public class LaserSequencer : Projectile
    {
        public GameObject laserHitDetector;

		float currentLifetime = 0.0f;

        public override float damage => 0;
        public override bool hostile => true;

        public override bool Hit(Vehicle vehicle)
        {
            return true;
        }

        public void Update()
		{
			currentLifetime += Time.deltaTime;

            if (currentLifetime >= 3.0 && laserHitDetector)
            {
                laserHitDetector.SetActive(true);
            }
            if (currentLifetime >= 5.0)
            {

                laserHitDetector.SetActive(false);
                this.gameObject.SetActive(false);

                Destroy(laserHitDetector);
                Destroy(this.gameObject);
            }
		}
	}
}

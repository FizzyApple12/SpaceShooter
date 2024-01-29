using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Vehicles;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Projectiles
{
	public class Missile : Projectile
	{
		public GameObject hitEffect;

		public override float damage => 100;
        public override bool hostile => false;

        public const float speed = 5.0f;

		GameObject target = null;

		float currentLifetime;

		public void Start()
		{
			FindTarget();
		}

		public void Update()
        {
            currentLifetime += Time.deltaTime;

            if (currentLifetime >= 10)
            {
                Destroy(this.gameObject);
            }

            if (target is null)
            {
                FindTarget();
			} else
			{
                if (target)
                {
                    Vector3 directionToTarget = transform.position - target.transform.position;
                    Quaternion targetRot = Quaternion.LookRotation(directionToTarget);
                    Quaternion clampedTargetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 90f * Time.deltaTime);
                } else
                {
                    target = null;
                }
            }

            this.transform.position += transform.forward * speed * Time.deltaTime;
		}

		public void FindTarget()
		{
            RaycastHit[] objectsInFront = Physics.SphereCastAll(transform.position, 0.2f, transform.forward, 10);

            if (objectsInFront.Length != 0)
            {
                float leastUnsignedAngle = float.MaxValue;
                int leastObjectIndex = 0;

                for (int i = 0; i < objectsInFront.Length; i++)
                {
                    float targetUnsignedAngle = Mathf.Abs(Vector3.SignedAngle(transform.forward, Vector3.Normalize(objectsInFront[i].point), transform.forward));

                    if (targetUnsignedAngle < leastUnsignedAngle && objectsInFront[i].transform.gameObject.tag == "Enemy")
                    {
                        leastUnsignedAngle = targetUnsignedAngle;
                        leastObjectIndex = i;
                    }
                }

                target = objectsInFront[leastObjectIndex].transform.gameObject;
            }
        }

		public override bool Hit(Vehicle vehicle)
		{
			Instantiate(hitEffect, this.transform.position, this.transform.rotation);

			return true;
		}
	}
}

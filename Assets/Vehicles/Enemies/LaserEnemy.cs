using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Vehicles
{
	public class LaserEnemy : Enemy
	{
		public override float health { get; set; } = 500;

		public override float pointValue => 100;

        public GameObject laserPrefab;

        public GameObject activeLaser;

        public const float speed = 0.30f;

        public const float fireCooldown = 7.0f;
        public const float fireDuration = 5.0f;
        float fireTimer = 0.0f;
        float aliveTime = 0.0f;
        bool hasFired = false;

        public override void OnStart()
        {
            fireTimer = fireCooldown + Random.Range(-1.0f, 1.0f);
        }

        public void Update()
        {
            aliveTime += Time.deltaTime;
            fireTimer -= Time.deltaTime;

            if (fireTimer <= 0.0f)
            {
                if (!hasFired)
                    activeLaser = Instantiate(laserPrefab, this.transform.TransformPoint(new Vector3(0f, 0f, 20f)), transform.rotation);

                hasFired = true;

                if (fireTimer <= -fireDuration)
                {
                    fireTimer = fireCooldown + Random.Range(-1.0f, 1.0f);
                    hasFired = false;

                    if (activeLaser != null) Destroy(activeLaser.gameObject);
                    activeLaser = null;
                }
            }

            Vector3 directionToTarget = player.gameObject.transform.position - transform.position;
            Quaternion targetRot = Quaternion.LookRotation(directionToTarget);
            Quaternion clampedTargetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 45f * Time.deltaTime);

            this.transform.rotation = clampedTargetRot;

            if (hasFired) return;

            float movementTime = Time.time;

            Vector3 movementDelta = transform.right + (Vector3.up * Mathf.Sin(movementTime));

            if (Mathf.Abs(directionToTarget.magnitude) > 2.0)
            {
                movementDelta += transform.forward;
            }
            else if (Mathf.Abs(directionToTarget.magnitude) < 1.5)
            {
                movementDelta -= transform.forward;
            }

            this.transform.position += movementDelta.normalized * speed * Time.deltaTime;
        }

        public override bool OnKill()
        {
            if (activeLaser != null) Destroy(activeLaser.gameObject);

            return true;
        }
    }
}

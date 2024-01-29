using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.GraphicsBuffer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Vehicles
{
	public class BasicEnemy : Enemy
	{
        public GameObject bulletPrefab;

		public override float health { get; set; } = 50;

		public override float pointValue => 10;

        public const float speed = 0.30f;

        public const float fireCooldown = 5.0f;
        float fireTimer = 0.0f;
        float aliveTime = 0.0f;

        bool escapeMode = false;

        Vector3 escapeTarget;

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
                Instantiate(bulletPrefab, this.transform.TransformPoint(new Vector3(0f, 0f, 20f)), transform.rotation);

                fireTimer = fireCooldown + Random.Range(-1.0f, 1.0f);
            }

            Vector3 directionToTarget = player.gameObject.transform.position - transform.position;

            if (!escapeMode)
            {
                Quaternion targetRot = Quaternion.LookRotation(directionToTarget);
                Quaternion clampedTargetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 45f * Time.deltaTime);

                this.transform.rotation = clampedTargetRot;

                this.transform.position += transform.forward * speed * Time.deltaTime;

                if (Mathf.Abs(directionToTarget.magnitude) <= 1.75)
                {
                    escapeMode = true;

                    float radialPosition = Random.Range(0, Mathf.PI * 2);
                    float height = Random.Range(0.5f, 2.0f);

                    escapeTarget =  new Vector3(2.5f * Mathf.Cos(radialPosition), height, 2.5f * Mathf.Sin(radialPosition));
                }
            } else
            {
                directionToTarget = escapeTarget - transform.position;

                Quaternion targetRot = Quaternion.LookRotation(directionToTarget);
                Quaternion clampedTargetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 45f * Time.deltaTime);

                this.transform.rotation = clampedTargetRot;

                this.transform.position += transform.forward * speed * Time.deltaTime;
                
                if (Mathf.Abs(directionToTarget.magnitude) < 0.1)
                {
                    escapeMode = false;
                }
            }

        }

		public override bool OnKill() 
        {
            return true;
        }
    }
}

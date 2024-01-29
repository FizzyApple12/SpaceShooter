using UnityEngine;

namespace Assets.Vehicles
{
	public abstract class Vehicle : MonoBehaviour
	{
		public abstract float health { get; set; }
		public abstract float pointValue { get; }

		public GameObject explosionPrefab;

		public abstract bool Kill();

		public void RecieveDamage(float damage)
		{
			health -= damage;

			if (health <= 0)
			{
				health = 0;

				Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);

				if (Kill())
                {
                    Destroy(this.gameObject);
                }
			}
		}
	}
}

using Assets.Vehicles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public abstract float damage { get; }
    public abstract bool hostile { get; }

    public abstract bool Hit(Vehicle vehicle);

	private void OnCollisionEnter(Collision collision)
	{
		Vehicle colliding_vehicle;

		if (collision.gameObject.TryGetComponent(out colliding_vehicle) && ((colliding_vehicle.tag == "Player" && hostile) || (colliding_vehicle.tag != "Player" && !hostile)))
		{
			colliding_vehicle.RecieveDamage(damage);

			if (Hit(colliding_vehicle))
			{
				Destroy(this.gameObject);
			}
		}
	}
}

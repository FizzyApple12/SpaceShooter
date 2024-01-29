using Assets.Vehicles;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : Vehicle
{
	private float _health = 1000;
	private float timeSinceLastDamage = 0;
	public override float health
	{
		get
		{
			return _health;
        }
		set
		{
			_health = value;
			timeSinceLastDamage = 0;
        }
	}

	public override float pointValue => 0;

	public GameObject playerControllingHand;

	public GameObject smallGunProjectile;
	public GameObject largeGunProjectile;

	public InputAction firePrimary;
	bool pressingPrimaryFire = false;

	public InputAction fireSecondary;
	bool pressingSecondaryFire = false;

	public const float primaryFireCooldown = 0.025f;
	int priamryFireIndex = 0;
	float primaryFireTimer = 0.0f;
	public Vector3[] primaryFireOffsets =
	{
		new Vector3(0, 0, 0),
		new Vector3(0, 0, 0),
		new Vector3(0, 0, 0),
		new Vector3(0, 0, 0)
	};

	public const float secondaryFireCooldown = 1f;
	int secondaryFireIndex = 0;
	float secondaryFireTimer = 0.0f;
	public Vector3[] secondaryFireOffsets =
	{
		new Vector3(0, 0, 0),
		new Vector3(0, 0, 0)
	};

	void Start()
	{
		firePrimary.Enable();
		fireSecondary.Enable();

		firePrimary.performed += ctx => { pressingPrimaryFire = ctx.ReadValue<bool>(); };
		fireSecondary.performed += ctx => { pressingSecondaryFire = ctx.ReadValue<bool>(); };
	}

	void Update()
	{
		Vector3 newPosition = playerControllingHand.transform.position - (0.08f * playerControllingHand.transform.up) + (0.08f * playerControllingHand.transform.forward);
		Quaternion newRotation = playerControllingHand.transform.rotation * Quaternion.Euler(80, 0, 0);

		this.transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10);
		this.transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10);

        timeSinceLastDamage += Time.deltaTime;

		if (timeSinceLastDamage >= 5.0)
		{
			_health = Mathf.Clamp(_health + (Time.deltaTime * 100), 0.0f, 1000.0f);
        }

        RaycastHit[] objectsInFront = Physics.SphereCastAll(transform.position, 0.2f, transform.forward, 10);

		bool targetFound = false;
		Vector3 targetPosition = Vector3.zero;

		if (objectsInFront.Length != 0)
		{
			targetFound = true;
			float leastUnsignedAngle = float.MaxValue;
			int leastObjectIndex = 0;

			for (int i = 0; i < objectsInFront.Length; i++)
			{
				//Debug.Log()

				float targetUnsignedAngle = Mathf.Abs(Vector3.SignedAngle(transform.forward, Vector3.Normalize(objectsInFront[i].point), transform.forward));

				if (targetUnsignedAngle < leastUnsignedAngle && objectsInFront[i].transform.gameObject.tag == "Enemy")
				{
					leastUnsignedAngle = targetUnsignedAngle;
					leastObjectIndex = i;
				}
			}

			targetPosition = objectsInFront[leastObjectIndex].point;
		}

		if (targetPosition == Vector3.zero) targetFound = false;

		primaryFireTimer -= Time.deltaTime;
		secondaryFireTimer -= Time.deltaTime;

		if ((firePrimary.IsPressed() || pressingPrimaryFire) && primaryFireTimer <= 0.0f)
		{
			GameObject projectile = Instantiate(smallGunProjectile, this.transform.TransformPoint(primaryFireOffsets[priamryFireIndex]), newRotation);
			priamryFireIndex = (priamryFireIndex + 1) % primaryFireOffsets.Length;
			primaryFireTimer = primaryFireCooldown;

			if (targetFound)
			{
				projectile.transform.LookAt(targetPosition);
			}
		}

		if ((fireSecondary.IsPressed() || pressingSecondaryFire) && secondaryFireTimer <= 0.0f)
		{
			GameObject projectile = Instantiate(largeGunProjectile, this.transform.TransformPoint(secondaryFireOffsets[secondaryFireIndex]), transform.rotation);
			secondaryFireIndex = (secondaryFireIndex + 1) % secondaryFireOffsets.Length;
			secondaryFireTimer = secondaryFireCooldown;

			if (targetFound)
			{
				projectile.transform.LookAt(targetPosition);
			}
		}
	}

	public override bool Kill()
	{
		GameManager.EndGame();

		health = 1000;

		return false;
	}

	public void SetVisibility(bool visible)
	{
		for (int i = 0; i < this.gameObject.transform.childCount; i++)
		{
			this.gameObject.transform.GetChild(i).gameObject.SetActive(visible);
		}
	}
}

using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Vehicles
{
    public abstract class Enemy : Vehicle
    {
        public abstract void OnStart();
        public abstract bool OnKill();


        public PlayerShip player;

        public void Start()
        {
            player = FindObjectOfType<PlayerShip>();
        }

        public override bool Kill()
        {
            if (OnKill())
            {
                GameManager.EnemyDied(pointValue);

                return true;
            }

            return false;
        }
    }
}

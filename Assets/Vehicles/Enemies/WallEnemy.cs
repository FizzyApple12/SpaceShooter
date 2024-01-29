using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Vehicles
{
	public class WallEnemy : Enemy
    {
		public override float health { get; set; } = 1000;

		public override float pointValue => 300;

		public void Update()
		{

		}

		public override bool OnKill()
        {
            return true;
        }

        public override void OnStart()
        {

        }
    }
}

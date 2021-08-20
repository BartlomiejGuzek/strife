using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.abilities
{
	class SniperActive : BaseAbility
	{
		public override string AbilityName { get; set; }
		public override short Cooldown { get; set; }
		public override bool IsActive { get; set; }

		public SniperActive()
		{
			AbilityName = "Active Test Ability";
			Cooldown = 20;
			IsActive = true;
		}
		public override void Fire()
		{
			Log.Info( AbilityName );
			
		}
	}
}

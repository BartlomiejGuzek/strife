using Sandbox;
using strife.player.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.abilities
{
	class SpyActiveAbility : BaseAbility
	{
		public override string AbilityName { get; set; }
		public override short Cooldown { get; set; }
		public override bool IsActive { get; set; }
		public TimeSince TimeSinceDisguise { get; set; } = 0;

		public SpyActiveAbility()
		{
			AbilityName = "Active Test Ability";
			Cooldown = 5;
			IsActive = true;
		}
		public void Fire( SpyClass spyPlayer )
		{
			TimeSinceDisguise = 0;
			spyPlayer.SetModel( "models/wooden_crate.vmdl" );
			GameTask.DelaySeconds( 5f );
			spyPlayer.SetModel( "models/citizen/citizen.vmdl" );
			Log.Info( AbilityName );
		}

		public override void Fire()
		{
			throw new NotImplementedException();
		}
	}
}

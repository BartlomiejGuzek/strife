using Sandbox;
using strife.player.abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.classes
{
	partial class SpyClass : StrifePlayer
	{
		public override float MovementSpeed { get; set; } = 200;
		public override float SprintSpeed { get; set; } = 200;
		public SpyActiveAbility ActiveAbility { get; set; }
		public SpyClass()
		{
			Inventory = new StrifePlayerInventory( this );
			CurrentClassName = "Spy";
			Hat = "models/citizen_clothes/hat/hat_leathercapnobadge.vmdl";
			ActiveAbility = new SpyActiveAbility();
			AbilityCooldown = ActiveAbility.Cooldown;
			TimeSinceAbilityUsed = 0;
		}
		public override void Respawn()
		{
			base.Respawn();
			Inventory.Add( new SMG(), true );
			GiveAmmo( AmmoType.Pistol, 100 );
			(Controller as StrifePlayerController).DefaultSpeed = MovementSpeed;
			(Controller as StrifePlayerController).SprintSpeed = SprintSpeed;
		}
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			if ( Input.Pressed( InputButton.Use ) )
			{
				if(!IsServer)
				{
					if ( CanUseAbility( AbilityCooldown ) )
					{
						ActiveAbility.Fire(this);
						TimeSinceAbilityUsed = 0;
					}
					else Log.Info( "On cooldown" );
				}
			}
		}
	}
}

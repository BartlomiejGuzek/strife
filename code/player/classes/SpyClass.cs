using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.classes
{
	partial class SpyClass : StrifePlayer
	{
		public override float MovementSpeed { get; set; } = 200;
		public override float SprintSpeed { get; set; } = 200;
		public TimeSince TimeSinceDisguise { get; set; } = 0;
		public int TimeInDisguise { get; set; } = 1500;
		override public short AbilityCooldown { get; set; } = 20;
		public SpyClass()
		{
			Inventory = new StrifePlayerInventory( this );
			CurrentClassName = "Spy";
			Hat = "models/citizen_clothes/hat/hat_leathercapnobadge.vmdl";
			TimeSinceAbilityUsed = 0;
		}
		public override void Respawn()
		{
			base.Respawn();
			SetModel( "models/citizen/citizen.vmdl" );
			Dress( CurrentTeam, Hat );
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
				/*if(!IsServer)
				{*/
					if ( CanUseAbility( AbilityCooldown ) )
					{
						FireAbility();
						TimeSinceAbilityUsed = 0;
					}
				//}
			}
			if( Input.Pressed( InputButton.Attack1 ) )
			{
				CleanDisguise();
			}
		}

		public override void TakeDamage( DamageInfo info )
		{
			base.TakeDamage( info );
			CleanDisguise();
		}
		public void FireAbility()
		{
			TimeSinceDisguise = 0;
			SetModel( "models/wooden_crate.vmdl" );
			UnDress();
		}
		public void CleanDisguise()
		{
			SetModel( "models/citizen/citizen.vmdl" );
			Dress( CurrentTeam, Hat );
		}
	}
}

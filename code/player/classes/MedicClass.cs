using Sandbox;
using strife.player.abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.classes
{
	partial class MedicClass : StrifePlayer
	{
		public override float MovementSpeed { get; set; } = 200;
		public override float SprintSpeed { get; set; } = 200;
		override public short AbilityCooldown { get; set; } = 1;
		public MedicClass()
		{
			Inventory = new StrifePlayerInventory( this );
			CurrentClassName = "Medic";
			Hat = "models/citizen_clothes/hat/hat_beret.red.vmdl";
		}
		public override void Respawn()
		{
			base.Respawn();
			SetModel( "models/citizen/citizen.vmdl" );
			Dress( CurrentTeam, Hat );
			Inventory.Add( new Pistol(), true );
			GiveAmmo( AmmoType.Pistol, 100 );
			(Controller as StrifePlayerController).DefaultSpeed = MovementSpeed;
			(Controller as StrifePlayerController).SprintSpeed = SprintSpeed;
		}
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			if ( Input.Pressed( InputButton.Use ) )
			{
				if ( CanUseAbility( AbilityCooldown ) )
				{
					FireAbility();
					TimeSinceAbilityUsed = 0;
				}
			}
		}

		public void FireAbility()
		{
			var ent = new Prop
			{
				Position = Vector3.Forward * 30,
				Rotation = Input.Rotation
			};
			ent.SetModel( "models/sbox_props/gas_cylinder_fat/gas_cylinder_fat.vmdl" );
			ent.Velocity = Owner.EyeRot.Forward * 10000;
		}
	}
}

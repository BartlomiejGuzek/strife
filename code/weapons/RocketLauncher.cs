using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.weapons
{
	[Library( "st_rocketlauncher", Title = "Rocket Launcher" )]
	[Hammer.EditorModel( "weapons/rust_crossbow/rust_crossbow.vmdl" )]
	partial class RocketLauncher : BaseStrifeWeapon
	{
		public override float PrimaryRate => 1.0f;
		public override float SecondaryRate => 1.0f;
		public override int ClipSize => 4;
		public override float ReloadTime => 6.0f;

		public override void Spawn()
		{
			base.Spawn();
			SetModel( "weapons/rust_smg/rust_smg.vmdl" );
			AmmoClip = 20;
		}
		public override void AttackPrimary()
		{
			TimeSincePrimaryAttack = 0;
			TimeSinceSecondaryAttack = 0;

			if ( !TakeAmmo( 1 ) )
			{
				DryFire();
				return;
			}

			(Owner as AnimEntity).SetAnimBool( "b_attack", true );

			//
			// Tell the clients to play the shoot effects
			//
			ShootEffects();
			PlaySound( "rust_smg.shoot" );

			//
			// Shoot the bullets
			//
			Rand.SetSeed( Time.Tick );
			//ShootBullet( 0.1f, 1.5f, 5.0f, 3.0f );
			ShootRocket( 0.1f, 1.5f, 5.0f, 3.0f );

		}

		[ClientRpc]
		protected override void ShootEffects()
		{
			Host.AssertClient();

			Particles.Create( "particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle" );
			Particles.Create( "particles/pistol_ejectbrass.vpcf", EffectEntity, "ejection_point" );

			if ( Owner == Local.Pawn )
			{
				new Sandbox.ScreenShake.Perlin( 0.5f, 2.0f, 4.0f, 2.0f );
			}

			ViewModelEntity?.SetAnimBool( "fire", true );
			CrosshairPanel?.CreateEvent( "fire" );
		}
	}
}

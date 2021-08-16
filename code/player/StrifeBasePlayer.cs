using Sandbox;
using strife;
using strife.player;
using System;
using System.Linq;
using System.Numerics;
using System.Threading;

partial class StrifeBasePlayer : Player
{
	
	TimeSince timeSinceDropped;
	DamageInfo LastDamage;
	public static bool Debug { get; set; } = true;
	public virtual int MaxHealth => 100;
	public bool SupressPickupNotices { get; private set; }

	public StrifeBasePlayer()
	{
		Inventory = new StrifePlayerInventory( this );
	}

	public override void Respawn()
	{
		SetModel( "models/citizen/citizen.vmdl" );

		Controller = new StrifePlayerController();
		Animator = new StandardPlayerAnimator();
		Camera = new ThirdPersonCamera();

		EnableAllCollisions = true; 
		EnableDrawing = true; 
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;

		ClearAmmo();
		SupressPickupNotices = true;
		Inventory.Add( new Pistol(), true );
		Inventory.Add( new Shotgun() );
		Inventory.Add( new SMG() );
		Inventory.Add( new Crossbow() );

		GiveAmmo( AmmoType.Pistol, 100 );
		GiveAmmo( AmmoType.Buckshot, 8 );
		GiveAmmo( AmmoType.Crossbow, 4 );

		SupressPickupNotices = false;
		Health = 100;
		base.Respawn();
	}
	public override void OnKilled()
	{
		base.OnKilled();

		Inventory.DropActive();
		Inventory.DeleteContents();

		BecomeRagdollOnClient( LastDamage.Force, GetHitboxBone( LastDamage.HitboxIndex ) );

		Controller = null;
		Camera = new SpectateRagdollCamera();

		EnableAllCollisions = false;
		EnableDrawing = false;
	}
	public override void Simulate( Client cl )
	{
		//if ( cl.NetworkIdent == 1 )
		//	return;
		base.Simulate( cl );

		//
		// Input requested a weapon switch
		//
		if ( Input.Pressed( InputButton.Jump ) )
		{
			if(!IsServer)
			{
				Log.Info( $"{ cl.Name } { "Has been called FFF" }" );

				if( menu != null )
				{
					menu.Disable();
					menu = null;
				}
				else if ( menu == null )
				{
					menu = new TeamMenu( cl );
					menu.Enable();
				}
			}

		}
		
		if ( Input.ActiveChild != null )
		{
			ActiveChild = Input.ActiveChild;
		}

		if ( LifeState != LifeState.Alive )
			return;

		TickPlayerUse();	
		SimulateActiveChild( cl, ActiveChild );
	}
	public override void StartTouch( Entity other )
	{
		if ( timeSinceDropped < 1 ) return;

		base.StartTouch( other );
	}
	public override void TakeDamage( DamageInfo info )
	{
		LastDamage = info;

		// hack - hitbox 0 is head
		// we should be able to get this from somewhere
		if ( info.HitboxIndex == 0 )
		{
			info.Damage *= 2.0f;
		}

		base.TakeDamage( info );

		if ( info.Attacker is StrifeBasePlayer attacker && attacker != this )
		{
			// Note - sending this only to the attacker!
			attacker.DidDamage( To.Single( attacker ), info.Position, info.Damage, Health.LerpInverse( 100, 0 ) );

			TookDamage( To.Single( this ), info.Weapon.IsValid() ? info.Weapon.Position : info.Attacker.Position );
		}
	}
	[ClientRpc]
	public void DidDamage( Vector3 pos, float amount, float healthinv )
	{
		Sound.FromScreen( "dm.ui_attacker" )
			.SetPitch( 1 + healthinv * 1 );

		HitIndicator.Current?.OnHit( pos, amount );
	}
	[ClientRpc]
	public void TookDamage( Vector3 pos )
	{
		//DebugOverlay.Sphere( pos, 5.0f, Color.Red, false, 50.0f );
		DamageIndicator.Current?.OnHit( pos );
	}
	public void Heal( short healAmount )
	{
		if (healAmount <= 0)
		{
			return;
		}
		else
			Health += healAmount;
	}
}

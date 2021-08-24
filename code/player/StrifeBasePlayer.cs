using Sandbox;
using strife;
using strife.player;
using strife.player.abilities;
using strife.player.classes;
using strife.ui;
using strife.weapons;
using System;
using System.Linq;
using System.Numerics;
using System.Threading;

partial class StrifeBasePlayer : Player
{
	public string CurrentClassName { get; set; } = "Class name";
	[Net]
	public new float Health { get; set; } = 100;
	virtual public float MovementSpeed { get; set; } = 200;
	virtual public float SprintSpeed { get; set; } = 200;
	public string Hat { get; set; } = "models/citizen_clothes/hat/hat_woolly.vmdl";
	[Net]
	public Team CurrentTeam { get; set; }
	[Net]
	public TimeSince TimeSinceAbilityUsed { get; set; }
	[Net]
	public short AbilityCooldown { get; set; } = 20;
	TimeSince timeSinceDropped;
	DamageInfo LastDamage;
	public TeamMenu teamMenu { get; set; }
	public ClassMenu classMenu { get; set; }
	public static bool Debug { get; set; } = true;
	public bool SupressPickupNotices { get; private set; }

	public StrifeBasePlayer()
	{
	}

	public override void Respawn()
	{
		//TODO Figure out how to change team here
		//GetCurrentTeam();
		Dress( CurrentTeam, Hat );
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
		//===============DEBUG SECTION===============
		if ( Debug )
		{
			var lineOffset = 0;
			//if ( Host.IsServer ) lineOffset = 10;
			DebugOverlay.ScreenText( lineOffset + 0, $"        Team name: {CurrentTeam}" );
			DebugOverlay.ScreenText( lineOffset + 1, $"        Class: {CurrentClassName}" );
			DebugOverlay.ScreenText( lineOffset + 2, $"        Health: {Health}" );
			DebugOverlay.ScreenText( lineOffset + 3, $"        Movement Speed: {MovementSpeed}" );
			DebugOverlay.ScreenText( lineOffset + 4, $"        Sprint Speed: {SprintSpeed}" );
			DebugOverlay.ScreenText( lineOffset + 5, $"        Sprint Speed: {AbilityCooldown}" );

			/*if(!IsServer)
			{
				if ( Input.Pressed( InputButton.Flashlight ) )
				{
					//TODO Fix team switching from one team to another. Player needs to receive new team after switching!
					if ( CurrentTeam == Team.Red )
						ChangeTeam( client, Team.Green );
					if ( CurrentTeam == Team.Green )
						ChangeTeam( client, Team.Red );
				}
			}*/

		}
		//===============DEBUG SECTION===============
		//
		// Input requested a weapon switch
		//
		if ( Input.Pressed( InputButton.Flashlight ) )
		{
			if(!IsServer)
			{
				this.teamMenu ??= new TeamMenu( cl );
				if ( this.teamMenu.IsVisible )
				{
					this.teamMenu.Disable();
				}
				else
				{
					this.teamMenu.Enable();
				}
			}
		}
		if ( Input.Pressed( InputButton.Drop ) )
		{
			if ( !IsServer )
			{
				this.classMenu ??= new ClassMenu( cl );
				if ( this.classMenu.IsVisible )
				{
					this.classMenu.Disable();
				}
				else
				{
					this.classMenu.Enable();
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
	public void ChangeClass( string className )
	{
		CurrentClassName = className;
	}
	public void GetCurrentTeam( Client client )
	{
		CurrentTeam = StrifeGame.GetPlayerTeam( client );
	}
	public void ChangeTeam( Client client, Team selectedTeam )
	{
		if ( StrifeGame.AssignPlayerToTeam( client, selectedTeam ) )
		{
			CurrentTeam = selectedTeam;
		}

	}
	public void UseAbility()
	{
		if ( TimeSinceAbilityUsed >= AbilityCooldown )
		{
			TimeSinceAbilityUsed = 0;
			//Fire ability
		}
	}
}

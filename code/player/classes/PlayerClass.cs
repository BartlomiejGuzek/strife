using Sandbox;
using strife.player.abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.classes
{
	partial class PlayerClass : StrifeBasePlayer
	{
		[Net]
		public PlayerClassData ClassData { get; set; }
		[Net]
		public Team CurrentTeam { get; set; }
		public TimeSince TimeSinceAbilityUsed { get; set; }
		public short AbilityCooldown { get; set; }
		[Net]
		public string CurrentClassName { get ; set; }
		[Net]
		public SniperActive ActiveAbility { get; set; }
		
		
		public PlayerClass()
		{
			Inventory = new StrifePlayerInventory( this );
			ClassData = null;
		}
		public PlayerClass( string className )
		{
			Inventory = new StrifePlayerInventory( this );
			CurrentClassName = className;
			GetClassData();
		}
		public void GetClassData()
		{
			ClassData = Resource.FromPath<PlayerClassData>( "data/classes/" + CurrentClassName + ".class" );
		}
		public void ChangeClass(string className )
		{
			CurrentClassName = className;
		}
		public void GetCurrentTeam(Client client)
		{
			CurrentTeam = StrifeGame.GetPlayerTeam(client);
		}
		public void ChangeTeam(Client client, Team selectedTeam)
		{
			if (StrifeGame.AssignPlayerToTeam( client, selectedTeam ))
			{
				CurrentTeam = selectedTeam;
			}
			
		}
		public void UseAbility()
		{
			if(TimeSinceAbilityUsed >= AbilityCooldown)
			{
				TimeSinceAbilityUsed = 0;
				ActiveAbility.Fire();
			}
		}
		public override void Respawn()
		{
			GetClassData();
			if(CurrentClassName == "Sniper")
			{
				ActiveAbility = new SniperActive();
			}
			//TODO Figure out how to change team here
			//GetCurrentTeam();
			base.Respawn();
			Dress(CurrentTeam, ClassData );
			(Controller as StrifePlayerController).DefaultSpeed = ClassData.MovementSpeed;
			Health = ClassData.Health;
			(Controller as StrifePlayerController).SprintSpeed = ClassData.SprintSpeed;
			AbilityCooldown = ClassData.AbilityCooldown;
		}
		public override void Simulate(Client client)
		{
			base.Simulate(client);
			
			//===============DEBUG SECTION===============
			if ( Debug )
			{
				var lineOffset = 0;
				//if ( Host.IsServer ) lineOffset = 10;
				DebugOverlay.ScreenText( lineOffset + 0, $"        Team name: {CurrentTeam}" );
			    DebugOverlay.ScreenText( lineOffset + 1, $"        Class: {ClassData.ClassName}" );
				DebugOverlay.ScreenText( lineOffset + 2, $"        Health: {ClassData.Health}" );
				DebugOverlay.ScreenText( lineOffset + 3, $"        Movement Speed: {ClassData.MovementSpeed}" );
				DebugOverlay.ScreenText( lineOffset + 4, $"        Sprint Speed: {ClassData.SprintSpeed}" );
				DebugOverlay.ScreenText( lineOffset + 5, $"        Sprint Speed: {ClassData.AbilityCooldown}" );

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
		}
	}
}

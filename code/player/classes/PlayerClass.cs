using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.classes
{
	partial class PlayerClass : StrifeBasePlayer
	{
		[Net, Predicted]
		public PlayerClassData ClassData { get; set; }
		[Net]
		public Team CurrentTeam { get; set; }
		[Net]
		public string CurrentClassName { get ; set; }
		public override int MaxHealth => ClassData != null ? ClassData.Health : base.MaxHealth;
		//public Ability MyProperty { get; set; }

		public PlayerClass()
		{
			Inventory = new StrifePlayerInventory( this );
			ClassData = null;
		}
		public PlayerClass( string className )
		{
			Inventory = new StrifePlayerInventory( this );
			CurrentClassName = className;
			GetCurrentClass();
		}
		public void GetCurrentClass()
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

		}
		public override void Respawn()
		{
			GetCurrentClass();
			//TODO Figure out how to change team here
			//GetCurrentTeam();
			base.Respawn();
			Dress(CurrentTeam, ClassData );
			(Controller as StrifePlayerController).DefaultSpeed = ClassData.MovementSpeed;
			Health = ClassData.Health;
			(Controller as StrifePlayerController).SprintSpeed = ClassData.SprintSpeed;

		}
		public override void Simulate(Client client)
		{
			base.Simulate(client);
			if(Input.Pressed(InputButton.Attack1))
			{
				ChangeClass("Sniper");
			}
			if (Input.Pressed( InputButton.Attack2))
			{
				ChangeClass( "Assault" );
			}
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

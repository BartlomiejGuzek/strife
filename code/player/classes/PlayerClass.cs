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
		[Net, Predicted]
		public Team CurrentTeam { get; set; }
		public override int MaxHealth => ClassData != null ? ClassData.Health : base.MaxHealth;


		public PlayerClass()
		{
			Inventory = new StrifePlayerInventory( this );
			ClassData = null;
		}
		public PlayerClass( string className )
		{
			Inventory = new StrifePlayerInventory( this );
			ClassData = Resource.FromPath<PlayerClassData>( "data/classes/" + className + ".class" );
		}

		public override void Respawn()
		{
			base.Respawn();
			Dress(CurrentTeam, ClassData );
			(Controller as StrifePlayerController).DefaultSpeed = ClassData.MovementSpeed;
			Health = ClassData.Health;
			(Controller as StrifePlayerController).SprintSpeed = ClassData.SprintSpeed;
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			//===============DEBUG SECTION===============
			if ( Debug )
			{
				var lineOffset = 0;
				//if ( Host.IsServer ) lineOffset = 10;
				DebugOverlay.ScreenText( lineOffset + 0, $"        Team name: {CurrentTeam}" );
			    DebugOverlay.ScreenText( lineOffset + 1, $"        Class: {ClassData.Name}" );
				DebugOverlay.ScreenText( lineOffset + 2, $"        Health: {ClassData.Health}" );
				DebugOverlay.ScreenText( lineOffset + 3, $"        Movement Speed: {ClassData.MovementSpeed}" );
				DebugOverlay.ScreenText( lineOffset + 4, $"        Sprint Speed: {ClassData.SprintSpeed}" );

				if ( Input.Pressed( InputButton.Flashlight ) )
				{
					//TODO Fix team switching from one team to another. Player needs to receive new team after switching!
					if ( CurrentTeam == Team.Red )
						StrifeGame.AssignPlayerToTeam( cl, Team.Green );
					if ( CurrentTeam == Team.Green )
						StrifeGame.AssignPlayerToTeam( cl, Team.Red );

				}
			}
			//===============DEBUG SECTION===============
			
			
		}
	}
}

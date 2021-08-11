using Sandbox;
using Sandbox.UI;
using strife;
using strife.ui;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// This is the heart of the gamemode. It's responsible
/// for creating the player and stuff.
/// </summary>
[Library( "Strife", Title = "Strife" )]
partial class StrifeGame : Game
{
	[Net]
	public Teams Teams { get; protected set; }
	public StrifeGame()
	{
		//
		// Create the HUD entity. This is always broadcast to all clients
		// and will create the UI panels clientside. It's accessible 
		// globally via Hud.Current, so we don't need to store it.
		//

		Teams = new Teams();

		if ( IsServer )
		{
			new PlayerHud();
			//ChatBox.AddInformation( "Change team" );
		}
	}

	public override void PostLevelLoaded()
	{
		base.PostLevelLoaded();

		ItemRespawn.Init();
	}

	public override void ClientJoined( Client cl )
	{
		var player = new StrifePlayer();
		//new TeamSelectionMenu();
		player.Respawn();
		cl.Pawn = player;
		base.ClientJoined( cl );
	}
	public static void ChangeTeam( Client client)
	{
		if ( client == null || Current is not StrifeGame strifeGame )
		{
			return;
		}
		strifeGame.Teams.AssignPlayer( client, Team.Green );
		//Log.Info( "change team" );
	}
	

}

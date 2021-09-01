using Sandbox;
using Sandbox.UI;
using strife;
using strife.player.classes;
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
		//TODO Get class from selection menu
		var teamNumer = Rand.Int( 1, 2 );

		//var teamMenu = new TeamMenu( cl );
		//teamMenu.Enable();
		var player = new MedicClass();
		player.CurrentTeam = GetPlayerTeam(cl);
		player.Respawn();
		cl.Pawn = player;
		base.ClientJoined( cl );

	}
	public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
	{
		RemovePlayerFromTeam( cl );
		base.ClientDisconnect( cl, reason );
	}
	public static bool AssignPlayerToTeam( Client client, Team teamName )
	{
		if ( client == null || Current is not StrifeGame strifeGame )
		{
			return false;
		}
		return strifeGame.Teams.AssignPlayer( client, teamName );
	}
	public static void RemovePlayerFromTeam( Client client )
	{
		if ( client == null || Current is not StrifeGame strifeGame )
		{
			return;
		}
		strifeGame.Teams.RemovePlayer( client );
	}

	public static bool AssignPlayerToClass( Client client, string className )
	{
		if ( client == null || Current is not StrifeGame strifeGame )
		{
			return false;
		}
		var player = new MedicClass(); //(StrifePlayer)Activator.CreateInstance( Type.GetType( $"strife.player.classes, {className}" ) );

		if ( Host.IsServer )
		{
			client.Pawn?.Delete();  //it has to be a server, but it is never here
		}

		player.CurrentTeam = GetPlayerTeam( client );
		client.Pawn = player;

		if ( Host.IsServer )
		{
			(client.Pawn as Player).Respawn();
		}

		return true;
	}
	//public static void RemovePlayerFromClass( Client client )
	//{
	//	if ( client == null || Current is not StrifeGame strifeGame )
	//	{
	//		return;
	//	}
	//	strifeGame.Teams.RemovePlayer( client );
	//}
	public static Team GetPlayerTeam(Client client )
	{
		if ( client == null || Current is not StrifeGame strifeGame )
		{
			return Team.Spectator;
		}
		return strifeGame.Teams.GetPlayerTeam( client );
	}


}

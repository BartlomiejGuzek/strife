using Sandbox;
using Sandbox.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife
{
	public enum Team : sbyte { Spectator = 0, Green = 1, Red = 2 }

	public partial class Teams : Entity
	{
		[Net]
		private List<Team> PlayerTeams { get; set; }
		[Net]
		private static Dictionary<Team, int> TeamPlayerCount { get; set; }
		public Teams()
		{
			PlayerTeams = new List<Team>( new Team[64] );
			TeamPlayerCount = new Dictionary<Team, int>();
			foreach ( Team team in Enum.GetValues( typeof( Team ) ))
			{
				TeamPlayerCount.Add( team, 0 );
			}
			Transmit = TransmitType.Always;
		}
		public bool AssignPlayer(Client client, Team teamName)
		{
			//TODO Check for team count. This will always return 'true' no matter what. We should check for few things here
			TeamPlayerCount[teamName] += 1;
			if ( GetPlayerTeam(client) != Team.Spectator)
			{
				RemovePlayer( client );
			}
			PlayerTeams[client.NetworkIdent - 1] = teamName;
			if ( !Host.IsServer )
			{
				ChatBox.AddInformation($"{ client.Name } { "Joined to " + teamName + " team" }", $"avatar:{ client.SteamId }" );
			}
			return true;
		}
		public bool RemovePlayer(Client client)
		{
			var currentTeam = GetPlayerTeam( client );
			TeamPlayerCount[currentTeam] -= 1;
			PlayerTeams[client.NetworkIdent - 1] = 0;
			Log.Info( $"{ client.Name } { "Has been removed from " + currentTeam + " team" }" );
			return true;
		}
		public Team GetPlayerTeam(Client client)
		{
			return PlayerTeams[client.NetworkIdent - 1];
		}

		public static int GetTeamPlayerCount( Team team )
		{
			TeamPlayerCount.TryGetValue( team, out int amount );
			return amount;
		}
	}
}

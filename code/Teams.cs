using Sandbox;
using Sandbox.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife
{
	public enum Team : sbyte { Unassigned = 0, Spectator = 1, Green = 2, Red = 3 }
	public partial class Teams : Entity
	{
		[Net]
		private List<Team> PlayerTeams { get; set; }
		public Teams()
		{
			PlayerTeams = new List<Team>( new Team[64] );
			Transmit = TransmitType.Always;
		}

		public bool AssignPlayer(Client client, Team teamName)
		{
			PlayerTeams[client.NetworkIdent - 1] = teamName;
			if ( Host.IsServer )
			{
				ChatBox.AddInformation( To.Everyone, $"{ client.Name } { "Joined to " + teamName + " team" }", $"avatar:{ client.SteamId }" );
			}
			return true;
		}

		public bool RemovePlayer(Client client, Team teamName )
		{

			return true;
		}

	}
}

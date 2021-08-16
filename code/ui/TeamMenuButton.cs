using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.ui
{
	public class TeamMenuButton : Button
	{
		protected Team team;
		private Client client;
		protected Color notHoveredColor_;
		protected Color hoveredColor_;

		protected Label playerCount_;

		public TeamMenuButton( Team team, Client client )
		{
			this.team = team;
			this.client = client;

			//notHoveredColor_ = Teams.GetDarkTeamColor( team );

			notHoveredColor_.a = 0.9f;

			string buttonLabelText = "";

			switch ( team )
			{
				case Team.Spectator:
					buttonLabelText = "Spectate";
					break;

				case Team.Red:
					buttonLabelText = "Join Red Team";
					break;

				case Team.Green:
					buttonLabelText = "Join Green Team";
					break;
			}

			hoveredColor_ = new Color(
				notHoveredColor_.r + 0.1f,
				notHoveredColor_.g + 0.1f,
				notHoveredColor_.b + 0.1f,
				0.9f );

			playerCount_ = Add.Label( "", "player-count" );
			Add.Label( buttonLabelText, "label" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( HasHovered )
			{
				Style.BackgroundColor = hoveredColor_;
			}
			else
			{
				Style.BackgroundColor = notHoveredColor_;
			}

			if ( team > 0 )
			{
				//playerCount_.Text = Teams.GetTeamPlayerCount( team_ ) + " players";
			}
		}

		protected override void OnClick( MousePanelEvent e )
		{
			base.OnClick( e );

			StrifeGame.AssignPlayerToTeam( client, team );
		}
	}
}

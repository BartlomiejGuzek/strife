using Sandbox;
using Sandbox.UI;
using System;

namespace strife.ui
{
	[Library]
	public class TeamMenu : HudEntity<RootPanel>
	{
		private Client client;

		public bool IsVisible { get; private set; }
		public TeamMenu(Client client)
		{
			this.client = client;
			IsVisible = false;
			if ( IsServer )
			{
				return;
			}
			RootPanel.StyleSheet.Load( "/styles/TeamMenu.scss" );
   
			Panel buttonPanel = RootPanel.AddChild<Panel>( "buttons" );

			Label label = RootPanel.AddChild<Label>( "select-team-label" );

			label.Text = "Select a Team";

			foreach ( Team team in Enum.GetValues( typeof( Team ) ) )
			{
				buttonPanel.AddChild( new TeamMenuButton( team, ClickEvent ) );
			}

			Disable();
		}

		public void Enable()
		{
			RootPanel.Style.Display = DisplayMode.Flex;
			RootPanel.Style.PointerEvents = "all";
			RootPanel.Style.Dirty();
			IsVisible = true;
		}

		public void Disable()
		{
			RootPanel.Style.Display = DisplayMode.None;
			RootPanel.Style.PointerEvents = "none";
			RootPanel.Style.Dirty();
			IsVisible = false;
		}

		public void ClickEvent( Team team )
		{
			StrifeGame.AssignPlayerToTeam( client, team );
			Disable();
		}
		
	}
}

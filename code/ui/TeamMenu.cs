using Sandbox;
using Sandbox.UI;

namespace strife.ui
{
	public class TeamMenu : HudEntity<RootPanel>
	{
		public TeamMenu(Client client)
		{
			RootPanel.StyleSheet.Load( @"ui\TeamMenu.scss" );
   
			Panel buttonPanel = RootPanel.AddChild<Panel>( "buttons" );

			Label label = RootPanel.AddChild<Label>( "select-team-label" );

			label.Text = "Select a Team";

			buttonPanel.AddChild( new TeamMenuButton( Team.Red, client ) );
			buttonPanel.AddChild( new TeamMenuButton( Team.Green, client ) );
			buttonPanel.AddChild( new TeamMenuButton( Team.Spectator, client ) );
		}

		public void Enable()
		{
			RootPanel.Style.Display = DisplayMode.Flex;
			RootPanel.Style.PointerEvents = "all";
			RootPanel.Style.Dirty();
		}

		public void Disable()
		{
			RootPanel.Style.Display = DisplayMode.None;
			RootPanel.Style.PointerEvents = "none";
			RootPanel.Style.Dirty();
		}
	}
}

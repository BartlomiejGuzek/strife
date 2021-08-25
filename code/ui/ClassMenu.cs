using Sandbox;
using Sandbox.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace strife.ui
{
	[Library]
	public class ClassMenu : HudEntity<RootPanel>
	{
		private Client client;
		private List<string> classes;

		public bool IsVisible { get; private set; }
		public ClassMenu( Client client )
		{
			this.client = client;
			IsVisible = false;
			classes = new List<string> { "Sniper", "Gruby", "Medyk" };
			//classes = Assembly.GetExecutingAssembly().GetTypes()
			//	.Where( t => t.IsClass && t.Namespace == "strife.player.classes" ).Select(t => t.Name.Replace("Class", "" ))
			//	.ToList();
			if ( IsServer )
			{
				return;
			}
			RootPanel.StyleSheet.Load( "/styles/ClassMenu.scss" );

			Panel buttonPanel = RootPanel.AddChild<Panel>( "buttons" );

			Label label = RootPanel.AddChild<Label>( "select-class-label" );

			label.Text = "Select Class";

			foreach ( string className in classes )
			{
				buttonPanel.AddChild( new ClassMenuButton( className, ClickEvent ) );
			}

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

		public void ClickEvent( string className)
		{
			StrifeGame.AssignPlayerToClass( client, className );
			Disable();
		}

	}
}

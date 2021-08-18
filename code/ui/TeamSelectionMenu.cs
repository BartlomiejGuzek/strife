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
	class TeamSelectionMenu : HudEntity<RootPanel>
	{
		public TeamSelectionMenu()
		{
			if ( !IsClient )
				return;
			//RootPanel.AddChild<TeamMenu>();
		}

	}
}

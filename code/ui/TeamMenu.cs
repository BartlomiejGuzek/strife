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

	class TeamMenu : Panel
	{
		private Button buttonGreen;
		private Button buttonRed;
		private Label playerCountGreen;
		private Label playerCountRed;

		public TeamMenu()
		{
			StyleSheet.Load( "/styles/_teamselectionmenu.scss" );
			Add.Panel( "menu" );
		}

	}
}

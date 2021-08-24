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
	public class ClassMenuButton : Button
	{
		protected Color notHoveredColor_;
		protected Color hoveredColor_;
		protected Label playerCount_;
		public delegate void ButtonClicked( string className );
		protected ButtonClicked clickEvent;
		public string className;

		public ClassMenuButton( string className, ButtonClicked clickEvent )
		{
			this.clickEvent = clickEvent;
			this.className = className;

			notHoveredColor_.a = 0.9f;

			string buttonLabelText = $"{className}";

			hoveredColor_ = new Color(
				notHoveredColor_.r + 0.1f,
				notHoveredColor_.g + 0.1f,
				notHoveredColor_.b + 0.1f,
				0.9f );

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

		}

		protected override void OnClick( MousePanelEvent e )
		{
			base.OnClick( e );
			clickEvent( className );
		}
	}
}

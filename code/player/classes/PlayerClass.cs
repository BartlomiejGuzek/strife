using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.classes
{
	class PlayerClass : StrifePlayer
	{
		public PlayerClassData ClassData { get; set; }


		public PlayerClass()
		{
			Inventory = new StrifePlayerInventory( this );
			ClassData = null;
		}
		public PlayerClass( string className )
		{
			Inventory = new StrifePlayerInventory( this );
			// Load the weapon data from a path
			ClassData = Resource.FromPath<PlayerClassData>( "data/classes/" + className + ".class" );
		}

		public override void Respawn()
		{
			base.Respawn();
			Dress( base.CurrentTeam, ClassData );

		}
	}
}

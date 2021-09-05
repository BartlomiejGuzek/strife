using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.abilities
{
	class HealingGrenade : Prop
	{
		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/rust_nature/rocks/rock_small_c_cave.vmdl" );
		}
	}
}

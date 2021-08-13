using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.classes
{
	[Library( "class" )]
	class PlayerClassData : Asset
	{
		public new string Name { get; set; } = "Class name";
		public int Health { get; set; } = 100;
		public float MovementSpeed { get; set; } = 200;
		public string Hat { get; set; } = "";
		//TODO abilities
	}
}

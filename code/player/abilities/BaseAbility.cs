using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.abilities
{
	class BaseAbility : Entity
	{
		public string AbilityName { get; set; }
		public short Cooldown { get; set; }
		public bool IsActive { get; set; }
	}
}

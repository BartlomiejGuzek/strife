using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.abilities
{
	abstract class BaseAbility
	{
		abstract public string AbilityName { get; set; }
		abstract public short Cooldown { get; set; }
		abstract public bool IsActive { get; set; }
		abstract public void Fire();
	}
}

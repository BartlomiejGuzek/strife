using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.abilities
{
	class SnipePassive : BaseAbility
	{
		public override string AbilityName { get; set; }
		public override short Cooldown { get; set; }
		public override bool IsActive { get; set; }

		public override void Fire()
		{
	
		}
	}
}

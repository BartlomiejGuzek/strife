using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.classes
{
	partial class SpyClass : StrifeBasePlayer
	{
		public override float MovementSpeed { get; set; } = 200;
		public override float SprintSpeed { get; set; } = 200;
		public SpyClass()
		{
			Inventory = new StrifePlayerInventory( this );
			CurrentClassName = "Spy";
			Hat = "models/citizen_clothes/hat/hat_leathercapnobadge.vmdl";
		}

		public override void Respawn()
		{
			base.Respawn();
			Inventory.Add( new SMG(), true );
			GiveAmmo( AmmoType.Pistol, 100 );
			(Controller as StrifePlayerController).DefaultSpeed = MovementSpeed;
			(Controller as StrifePlayerController).SprintSpeed = SprintSpeed;
		}
	}
}

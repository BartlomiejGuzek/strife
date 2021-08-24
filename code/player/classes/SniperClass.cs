using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.classes
{
	partial class SniperClass : StrifeBasePlayer
	{
		public override float MovementSpeed { get; set; } = 200;
		public override float SprintSpeed { get; set; } = 200;
		public SniperClass()
		{
			Inventory = new StrifePlayerInventory( this );
			CurrentClassName = "Sniper";
			Hat = "models/citizen_clothes/hat/hat_woolly.vmdl";
		}

		public override void Respawn()
		{
			base.Respawn();

			Inventory.Add( new Pistol(), true );
			GiveAmmo( AmmoType.Pistol, 100 );
			(Controller as StrifePlayerController).DefaultSpeed = MovementSpeed;
			(Controller as StrifePlayerController).SprintSpeed = SprintSpeed;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strife.player.classes
{
	partial class MedicClass : StrifeBasePlayer
	{
		public override float MovementSpeed { get; set; } = 200;
		public override float SprintSpeed { get; set; } = 200;
		public MedicClass()
		{
			Inventory = new StrifePlayerInventory( this );
			CurrentClassName = "Medic";
			Hat = "models/citizen_clothes/hat/hat_beret.red.vmdl";
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

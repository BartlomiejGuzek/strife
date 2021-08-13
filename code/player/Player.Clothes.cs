using Sandbox;
using strife;
using strife.player.classes;
using System.Reflection;

partial class StrifePlayer
{
	ModelEntity pants;
	ModelEntity jacket;
	ModelEntity shoes;
	ModelEntity hat;

	bool dressed = false;

	public void Dress(Team team, PlayerClassData playerClassData)
	{
		//TODO This bitch doesnt want to strip old clothes on respawn
		//dressed = false;
		if ( dressed ) return;
		dressed = true;

		if ( true )
		{
			var model = "models/citizen_clothes/trousers/trousers.lab.vmdl";

			pants = new ModelEntity();
			pants.SetModel( model );
			pants.SetParent( this, true );
			pants.EnableShadowInFirstPerson = true;
			pants.EnableHideInFirstPerson = true;
			pants.
			SetBodyGroup( "Legs", 1 );
		}

		if ( true )
		{
			var model = "";
			if (team == Team.Red)
{
				model = "models/citizen_clothes/jacket/jacket.red.vmdl";
			}
			else
				model = "models/citizen_clothes/jacket/jacket_heavy.vmdl";

			jacket = new ModelEntity();
			jacket.SetModel( model );
			jacket.SetParent( this, true );
			jacket.EnableShadowInFirstPerson = true;
			jacket.EnableHideInFirstPerson = true;

			var propInfo = jacket.GetModel().GetPropData();
			if ( propInfo.ParentBodyGroupName != null )
			{
				SetBodyGroup( propInfo.ParentBodyGroupName, propInfo.ParentBodyGroupValue );
			}
			else
			{
				SetBodyGroup( "Chest", 0 );
			}
		}

		if ( true )
		{
			var model = "models/citizen_clothes/shoes/shoes.workboots.vmdl";

			shoes = new ModelEntity();
			shoes.SetModel( model );
			shoes.SetParent( this, true );
			shoes.EnableShadowInFirstPerson = true;
			shoes.EnableHideInFirstPerson = true;

			SetBodyGroup( "Feet", 1 );
		}

		if ( true )
		{
			var model = playerClassData.Hat;

			hat = new ModelEntity();
			hat.SetModel( model );
			hat.SetParent( this, true );
			hat.EnableShadowInFirstPerson = true;
			hat.EnableHideInFirstPerson = true;
		}
		//UnDress();
	}
	public void UnDress()
	{
		pants.Delete();
		jacket.Delete();
		shoes.Delete();
		hat.Delete();
	}
}

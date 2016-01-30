using UnityEngine;
using System.Collections;

public class SetClothes : Action 
{
	public bool hasClothes;
	
	public override void DoAction ()
	{
		GameplayManager.instance.hasClothes = hasClothes;	
	}
}

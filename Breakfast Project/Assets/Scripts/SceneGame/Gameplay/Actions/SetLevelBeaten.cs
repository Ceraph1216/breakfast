using UnityEngine;
using System.Collections;

public class SetLevelBeaten : Action 
{
	public string levelBeaten;
	
	public override void DoAction ()
	{
		GameplayManager.instance.lastLevelbeaten = levelBeaten;	
	}
}

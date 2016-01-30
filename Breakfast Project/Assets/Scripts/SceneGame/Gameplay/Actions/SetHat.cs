using UnityEngine;
using System.Collections;

public class SetHat : Action 
{
	public bool hasHat;
	
	public override void DoAction ()
	{
		GameplayManager.instance.hasHat = hasHat;	
	}
}

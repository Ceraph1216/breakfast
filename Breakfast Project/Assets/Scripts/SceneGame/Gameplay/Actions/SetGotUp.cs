using UnityEngine;
using System.Collections;

public class SetGotUp : Action 
{
	public bool gotUp;
	
	public override void DoAction ()
	{
		GameplayManager.instance.gotUp = gotUp;	
	}
}

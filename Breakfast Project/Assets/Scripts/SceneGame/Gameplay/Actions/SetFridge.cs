using UnityEngine;
using System.Collections;

public class SetFridge : Action 
{
	public bool hasMilk;
	
	public override void DoAction ()
	{
		GameplayManager.instance.hasMilk = hasMilk;	
	}
}

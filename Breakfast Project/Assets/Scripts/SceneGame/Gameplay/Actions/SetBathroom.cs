using UnityEngine;
using System.Collections;

public class SetBathroom : Action 
{
	public bool brushedTeeth;
	
	public override void DoAction ()
	{
		GameplayManager.instance.brushedTeeth = brushedTeeth;	
	}
}

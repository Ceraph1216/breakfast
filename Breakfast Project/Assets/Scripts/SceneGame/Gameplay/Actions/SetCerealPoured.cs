using UnityEngine;
using System.Collections;

public class SetCerealPoured : Action 
{
	public bool pouredCereal;
	
	public override void DoAction ()
	{
		GameplayManager.instance.pouredCereal = pouredCereal;	
	}
}

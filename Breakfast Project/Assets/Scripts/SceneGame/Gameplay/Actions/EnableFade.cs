using UnityEngine;
using System.Collections;

public class EnableFade : Action 
{
	public FadeImage fade;
	
	public override void DoAction ()
	{
		fade.enabled = true;	
	}
}

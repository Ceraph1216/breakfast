using UnityEngine;
using System.Collections;

public class SetGameObjectActive : Action 
{
	public bool active;
	public GameObject objectToEnable;
	
	public override void DoAction ()
	{
		objectToEnable.SetActive (active);	
	}
}

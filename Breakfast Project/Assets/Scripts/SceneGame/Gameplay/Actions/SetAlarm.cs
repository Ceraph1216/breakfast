using UnityEngine;
using System.Collections;

public class SetAlarm : Action 
{
	public bool alarmStopped;
	
	public override void DoAction ()
	{
		GameplayManager.instance.stoppedAlarm = alarmStopped;	
	}
}

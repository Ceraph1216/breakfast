using UnityEngine;
using System.Collections;

public class ClearText : Action 
{
	public Action actionInQueue;

	private TopDownPlayer _player;

	void Awake ()
	{
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<TopDownPlayer> ();	
	}

	public override void DoAction ()
	{
		_player.UnfreezePlayer ();
		_player.canInteract = true;
		TextManager.instance.ClearText ();

		if (actionInQueue != null)
		{
			_player.currentAction = actionInQueue;	
		} else
		{
			_player.currentAction = null;	
		}
	}
}

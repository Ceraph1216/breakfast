using UnityEngine;
using System.Collections;

public class SetText : Action 
{
	public bool freezePlayer;
	public string text;
	public Action actionInQueue;
	
	private TopDownPlayer _player;
	
	void Awake ()
	{
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<TopDownPlayer> ();	
	}
	
	public override void DoAction ()
	{
		if (freezePlayer)
		{
			_player.FreezePlayer ();
		}
		_player.canInteract = true;
		TextManager.instance.SetText (text);
		
		if (actionInQueue != null)
		{
			_player.currentAction = actionInQueue;	
		} else
		{
			_player.currentAction = null;	
		}
	}
}

using UnityEngine;
using System.Collections;

public class ClearText : Interactable 
{
	public Interactable actionInQueue;

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
			_player.currentInteractable = actionInQueue;	
		} else
		{
			_player.currentInteractable = null;	
		}
	}
}

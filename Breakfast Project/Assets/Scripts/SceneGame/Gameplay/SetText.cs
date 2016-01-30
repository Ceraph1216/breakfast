using UnityEngine;
using System.Collections;

public class SetText : Interactable 
{
	public bool freezePlayer;
	public string text;
	public Interactable actionInQueue;
	
	private TopDownPlayer _player;
	
	void Awake ()
	{
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<TopDownPlayer> ();	
	}
	
	public override void DoAction ()
	{
		_player.FreezePlayer ();
		_player.canInteract = true;
		TextManager.instance.SetText (text);
		
		if (actionInQueue != null)
		{
			_player.currentInteractable = actionInQueue;	
		} else
		{
			_player.currentInteractable = null;	
		}
	}
}

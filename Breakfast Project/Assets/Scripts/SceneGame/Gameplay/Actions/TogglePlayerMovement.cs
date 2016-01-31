using UnityEngine;
using System.Collections;

public class TogglePlayerMovement : Action 
{
	public bool doImmediately;
	public bool topDown;
	public bool start;

	private Player _player;
	private TopDownPlayer _TDPlayer;
	
	void Awake ()
	{
		GameObject l_playerGO = GameObject.FindGameObjectWithTag ("Player");
		_player = l_playerGO.GetComponent<Player>();	
		_TDPlayer = l_playerGO.GetComponent<TopDownPlayer> ();
	}

	void OnEnable ()
	{
		if (doImmediately)
		{
			ToggleMovement ();
		}
	}

	public override void DoAction ()
	{
		ToggleMovement ();
	}
	
	private void ToggleMovement ()
	{
		if (start)
		{
			if (topDown)
			{
				_TDPlayer.UnfreezePlayer ();
			} else
			{
				_player.UnfreezePlayer ();
			}
		} 
		else
		{
			if (topDown)
			{
				_TDPlayer.FreezePlayer ();
			} 
			else
			{
				_player.FreezePlayer ();
			}
		}
		
	}
}

using UnityEngine;
using System.Collections;

public class PlayerCollisionScript : MonoBehaviour 
{
	private Player _player;
	
	void Awake() 
	{
		_player = GetComponent<Player> ();
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		// Check collision stuff
	}

	void OnTriggerEnter2D(Collider2D p_trig)
	{
		if (p_trig.gameObject.layer == Constants.HAZARD_LAYER_ID)
		{
			_player.Kill ();
		} 
		else if (p_trig.gameObject.layer == Constants.GOAL_LAYER_ID)
		{
			_player.FreezePlayer ();
		}
	}
}

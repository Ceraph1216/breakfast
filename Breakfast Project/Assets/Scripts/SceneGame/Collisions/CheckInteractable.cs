using UnityEngine;
using System.Collections;

public class CheckInteractable : MonoBehaviour 
{
	private TopDownPlayer _player;
	
	void Awake ()
	{
		_player = GetComponent<TopDownPlayer> ();	
	}

	void OnTriggerEnter2D(Collider2D p_trig)
	{
		if (p_trig.gameObject.layer == Constants.INTERACT_LAYER_ID)
		{
			Action l_interact = p_trig.GetComponent<Action> ();
//			Debug.Log ("getting interactable: " + l_interact + " from: " + p_trig.name);
			
			if (_player.currentAction == null)
			{
				_player.currentAction = l_interact;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D p_trig)
	{
		if (p_trig.gameObject.layer == Constants.INTERACT_LAYER_ID)
		{
			_player.currentAction = null;
		}
	}
}

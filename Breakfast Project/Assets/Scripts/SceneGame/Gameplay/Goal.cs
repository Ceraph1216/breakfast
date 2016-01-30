using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour 
{
	public Action[] actionsOnWin;
	
	void OnTriggerEnter2D(Collider2D p_trig)
	{
		if (p_trig.gameObject.layer == Constants.PLAYER_LAYER_ID)
		{
			Action l_interact = p_trig.GetComponent<Action> ();
			
			for (int i = 0; i < actionsOnWin.Length; i++)
			{
				actionsOnWin [i].DoAction ();	
			}
		}
	}
}

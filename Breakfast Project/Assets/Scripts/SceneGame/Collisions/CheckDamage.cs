using UnityEngine;
using System.Collections;

public class CheckDamage : MonoBehaviour 
{
	private Player _player;

	void Awake() 
	{
		_player = GetComponentInParent<Player> ();
	}

	void OnTriggerEnter2D(Collider2D p_trig)
	{
		if (p_trig.gameObject.layer == Constants.HAZARD_LAYER_ID)
		{
			_player.Kill ();
//			PrefabManager.instance.FindPoolForObject (p_trig.gameObject).Unspawn (p_trig.gameObject);
		} 
	}
}

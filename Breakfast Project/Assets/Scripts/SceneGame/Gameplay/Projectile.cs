using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public Vector3 speed;
	public float lifetime;
	
	private float _currentLifetime;
	
	private Transform _transform;

	void Awake ()
	{
		_transform = transform;	
	}
	
	void OnEnable () 
	{
		SoftPauseScript.instance.SoftUpdate += SoftUpdate;
		_currentLifetime = 0;
	}

	void OnDisable () 
	{
		SoftPauseScript.instance.SoftUpdate -= SoftUpdate;
	}

	// Update is called once per frame
	void SoftUpdate (GameObject dispatcher)
	{
		if (_currentLifetime >= lifetime)
		{
			PrefabManager.instance.FindPoolForObject (gameObject).Unspawn (gameObject);
			return;
		}
		
		_transform.Translate (speed * Time.deltaTime);
		
		_currentLifetime += Time.deltaTime;
	}
}

using UnityEngine;
using System.Collections;

public class SpawnProjectiles : MonoBehaviour 
{
	public float interval;
	public float startDelay;
	public Vector3 projectileSpeed;
	public float projectileLifetime;
	
	private float _currentDelay;
	private float _currentInterval;
	
	private Transform _transform;
	
	void Awake ()
	{
		_transform = transform;
	}
	
	void OnEnable () 
	{
		SoftPauseScript.instance.SoftUpdate += SoftUpdate;
		_currentDelay = 0;
		_currentInterval = 0;
	}

	void OnDisable () 
	{
		SoftPauseScript.instance.SoftUpdate -= SoftUpdate;
	}

	// Update is called once per frame
	void SoftUpdate (GameObject dispatcher)
	{
		if (_currentDelay < startDelay)
		{
			_currentDelay += Time.deltaTime;
			return;
		}
		
		if (_currentInterval >= interval)
		{
			Projectile l_newProjectile = PrefabManager.instance.projectilePool.Spawn (_transform.position, Quaternion.identity).GetComponent<Projectile>();
			l_newProjectile.speed = projectileSpeed;
			l_newProjectile.lifetime = projectileLifetime;
			
			_currentInterval = 0;
			return;
		}
		
		_currentInterval += Time.deltaTime;
	}
}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public RuntimeAnimatorController messyHeadController;
	public RuntimeAnimatorController hatHeadController;
	public RuntimeAnimatorController pjBodyController;
	public RuntimeAnimatorController normalBodyController;

	public Animator headAnimator;
	public Animator bodyAnimator;
	
	public ParticleSystem deathEffect;
	
	private bool _dead;
	public bool dead
	{
		get
		{
			return _dead;
		}
		set
		{
			_dead = value;
		}
	}

	private SpriteRenderer[] _spriteRenderers;
	private Transform _transform;
	private Rigidbody2D _rigidbody2D;
	private BasicPlayerMovementScript _movement;
	private Respawn _respawn;

	void Awake ()
	{
		_spriteRenderers = GetComponentsInChildren<SpriteRenderer> ();
		_rigidbody2D = GetComponent<Rigidbody2D> ();
		_transform = transform;
		_respawn = GetComponent<Respawn> ();
		_movement = GetComponent<BasicPlayerMovementScript> ();
	}
	
	void OnEnable ()
	{
		CheckAnimators ();	
	}

	public void Kill ()
	{
		if (dead)
		{
			return;
		}
		
		// TODO: remove enabling and disabling of game object when Unity releases 5.3.2 and fixes the errors being logged
		deathEffect.gameObject.SetActive (true);
		deathEffect.Play ();
		deathEffect.enableEmission = true;
		
		for (int i = 0; i < _spriteRenderers.Length; i++)
		{
			_spriteRenderers [i].enabled = false;	
		}
		_rigidbody2D.isKinematic = true;
		_movement.enabled = false;
		dead = true;

		StartCoroutine (SpawnPlayer ());
	}
	
	public void FreezePlayer ()
	{
		_movement.StopMovement ();
	}
	
	public void UnfreezePlayer ()
	{
		_movement.StartMovement ();	
	}

	private IEnumerator SpawnPlayer ()
	{
		yield return new WaitForSeconds (2f);

		deathEffect.gameObject.SetActive (false);
		_respawn.RespawnPlayer (_transform, _spriteRenderers, _rigidbody2D, _movement);
		dead = false;
	}
	
	private void CheckAnimators ()
	{
		if (GameplayManager.instance.hasHat)
		{
			headAnimator.runtimeAnimatorController = hatHeadController;
		} else
		{
			headAnimator.runtimeAnimatorController = messyHeadController;
		}

		if (GameplayManager.instance.hasClothes)
		{
			bodyAnimator.runtimeAnimatorController = normalBodyController;	
		} else
		{
			bodyAnimator.runtimeAnimatorController = pjBodyController;
		}
	}
}

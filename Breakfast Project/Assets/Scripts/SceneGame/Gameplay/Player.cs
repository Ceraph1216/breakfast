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
		// TODO: have death effect play
		
		for (int i = 0; i < _spriteRenderers.Length; i++)
		{
			_spriteRenderers [i].enabled = false;	
		}
		_rigidbody2D.isKinematic = true;
		_movement.enabled = false;

		StartCoroutine (SpawnPlayer ());
	}

	private IEnumerator SpawnPlayer ()
	{
		yield return new WaitForSeconds (2f);

		_respawn.RespawnPlayer (_transform, _spriteRenderers, _rigidbody2D, _movement);
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

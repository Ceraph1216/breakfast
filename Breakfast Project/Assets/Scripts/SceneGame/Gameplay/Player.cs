using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public ParticleSystem deathEffect;

	private SpriteRenderer _spriteRenderer;
	private Transform _transform;
	private Rigidbody2D _rigidbody2D;
	private BasicPlayerMovementScript _movement;
	private Respawn _respawn;

	void Awake ()
	{
		_spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		_rigidbody2D = GetComponent<Rigidbody2D> ();
		_transform = transform;
		_respawn = GetComponent<Respawn> ();
		_movement = GetComponent<BasicPlayerMovementScript> ();
	}

	public void Kill ()
	{
		// TODO: have death effect play

		_spriteRenderer.enabled = false;
		_rigidbody2D.isKinematic = true;
		_movement.enabled = false;

		StartCoroutine (SpawnPlayer ());
	}

	private IEnumerator SpawnPlayer ()
	{
		yield return new WaitForSeconds (2f);

		_respawn.RespawnPlayer (_transform, _spriteRenderer, _rigidbody2D, _movement);
	}
}

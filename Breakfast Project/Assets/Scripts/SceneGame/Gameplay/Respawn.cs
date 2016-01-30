using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour 
{
	public Transform spawnPoint;

	public void RespawnPlayer (Transform p_playerTransform, SpriteRenderer p_playerSprite, Rigidbody2D p_playerRigidbody, BasicPlayerMovementScript p_playerMovement)
	{
		p_playerTransform.position = spawnPoint.position;
		p_playerSprite.enabled = true;
		p_playerRigidbody.isKinematic = false;
		p_playerMovement.enabled = true;
	}
}

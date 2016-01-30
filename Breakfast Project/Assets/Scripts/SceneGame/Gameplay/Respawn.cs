using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour 
{
	public Transform spawnPoint;

	public void RespawnPlayer (Transform p_playerTransform, SpriteRenderer[] p_playerSprites, Rigidbody2D p_playerRigidbody, BasicPlayerMovementScript p_playerMovement)
	{
		for (int i = 0; i < p_playerSprites.Length; i++)
		{
			p_playerSprites[i].enabled = true;
		}
		
		p_playerTransform.position = spawnPoint.position;
		p_playerRigidbody.isKinematic = false;
		p_playerMovement.enabled = true;
	}
}

﻿using UnityEngine;
using System.Collections;

public class BasicPlayerMovementScript : MonoBehaviour 
{
//	public Sprite mySprite;
	public LayerMask groundCheckMask;
	
	public float maxJumpTime;
	
	public Transform leftSensorTransform;
	public Transform rightSensorTransform;
	public Transform groundSensorTransform;

	public Transform hitboxes;

	public GameObject attackHitbox;

	private bool isHitR;
	private bool isHitL;
	private bool isHitG;
	private bool isHitForward;
	
	private Vector2 hitPointR = Vector2.zero;
	private Vector2 hitPointL = Vector2.zero;
	private Vector2 hitPointG = Vector2.zero;
	private Vector2 hitPointForward = Vector2.zero;
	
	private float _currentJumpTime;
	private bool _canJump;

	private Rigidbody2D myRigidbody;
	private Transform myTransform;
	private BoxCollider2D _hitbox;
	
	private bool _canMove;

	void Awake ()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		myTransform = transform;
		_hitbox = GetComponent<BoxCollider2D> ();
	}

	void OnEnable () 
	{
		SoftPauseScript.instance.SoftUpdate += SoftUpdate;
		_canMove = true;
		_canJump = true;
	}
	
	void OnDisable () 
	{
		SoftPauseScript.instance.SoftUpdate -= SoftUpdate;
	}
	
	public void StopMovement ()
	{
		myRigidbody.velocity = Vector3.zero;
		myRigidbody.isKinematic = true;
		_canMove = false;
	}
	
	public void StartMovement ()
	{
		myRigidbody.isKinematic = false;
		_canMove = true;	
	}
	
	// Update is called once per frame
	void SoftUpdate (GameObject dispatcher) 
	{
		DrawRays();
		
		if (!_canMove)
		{
			return;	
		}

		// Get current velocity
		Vector3 newVelocity = myRigidbody.velocity;

		// Listen for button presses
		if (Input.GetButton("Jump"))
		{
//			if (PlayerStateManager.instance.currentGroundState == Enums.PlayerGroundState.OnGround)
//			{
				_currentJumpTime += Time.deltaTime;
				if (PlayerStateManager.instance.currentGroundState == Enums.PlayerGroundState.OnGround)
				{
//					_currentJumpTime = 0f;
				}

				
				if (_currentJumpTime >= maxJumpTime)
				{
					_canJump = false;
				}
				
				if (_canJump)
				{
					newVelocity = myRigidbody.velocity;
					newVelocity.y =  Constants.JUMP_FORCE;
					myRigidbody.velocity = newVelocity;
				}
//			}
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			Attack();
		}

		// Apply horizontal movement
		newVelocity = myRigidbody.velocity;
		newVelocity.x =  Input.GetAxis("Horizontal") * Constants.RUN_SPEED;
		
		// If we let go of the jump key cancel our ability to jump until we land again
		if (Input.GetButtonUp("Jump"))
		{
			_canJump = false;
			
			// make the arc a little smaller to allow for short hopping
			if (myRigidbody.velocity.y > 0)
			{
				myRigidbody.AddForce (Vector2.down * 400f);
			}
		}

		// If our front sensor hit something
		if (isHitForward)
		{
			// If we're trying to move toward the point we hit cancel movement
			if (Mathf.Sign(newVelocity.x) == Mathf.Sign(myTransform.localScale.x))
			{
				newVelocity.x = 0;
			}
		}
		myRigidbody.velocity = newVelocity;

		// Make sure the sprite is facing the right way
		if (myTransform.localScale.x > 0) // facing right
		{
			if (Input.GetAxis("Horizontal") < 0) // Moving left
			{
				Vector3 newScale = myTransform.localScale;
				newScale.x *= -1;
				myTransform.localScale = newScale;
				hitboxes.localScale = newScale;
			}
		}

		if (myTransform.localScale.x < 0) // facing left
		{
			if (Input.GetAxis("Horizontal") > 0) // Moving right
			{
				Vector3 newScale = myTransform.localScale;
				newScale.x *= -1;
				myTransform.localScale = newScale;
				hitboxes.localScale = newScale;
			}
		}

		//----------------------
		// Set states
		//----------------------

		// Set in air states

		if (!isHitG)
		{
			if (myRigidbody.velocity.y > 0.5f)
			{
				if (PlayerStateManager.instance.currentGroundState != Enums.PlayerGroundState.Rising)
				{
					PlayerStateManager.instance.currentGroundState = Enums.PlayerGroundState.Rising;
				}
			}
			else if (myRigidbody.velocity.y < -0.5f)
			{
				if (PlayerStateManager.instance.currentGroundState != Enums.PlayerGroundState.Falling)
				{
					PlayerStateManager.instance.currentGroundState = Enums.PlayerGroundState.Falling;
				}
			}
		}
		else
		{
			if (PlayerStateManager.instance.currentGroundState != Enums.PlayerGroundState.OnGround)
			{
				PlayerStateManager.instance.currentGroundState = Enums.PlayerGroundState.OnGround;
				_canJump = true;
				_currentJumpTime = 0f;
			}
		}

		// Set running state
		if (Mathf.Abs(myRigidbody.velocity.x) > 0.5f && !PlayerStateManager.instance.isMoving)
		{
			PlayerStateManager.instance.isMoving = true;
		}
		else if (Mathf.Abs(myRigidbody.velocity.x) <= 0.5f && PlayerStateManager.instance.isMoving)
		{
			PlayerStateManager.instance.isMoving = false;
		}
	}

	void DrawRays()
	{
		//---------------------------------------------------------------------
		// Use raycasting to find where the ground is in relation to the player
		// and what angle the ground they are on is.
		//---------------------------------------------------------------------
		
		// Set the direction and distance of the ground check
		Vector2 downDirection = new Vector2(0, -1);// -myTransform.up;
		Vector2 forwardDirection = new Vector2(myTransform.localScale.x, 0);
		float distanceSide = 1.5f;
		float distanceGround = 1.5f;
		float distanceFront = (_hitbox.size.x / 2f) + 0.5f;

		// Get the start positions for the two forward checks
		Vector3 l_forwardStartT = myTransform.position;
		l_forwardStartT.y += _hitbox.size.y;
		Vector3 l_forwardStartB = myTransform.position;

		// Create debug visualizations of the rays being used
		Debug.DrawRay (rightSensorTransform.position, downDirection * distanceSide, Color.green);
		Debug.DrawRay (leftSensorTransform.position, downDirection * distanceSide, Color.green);
		Debug.DrawRay (groundSensorTransform.position, downDirection * distanceGround, Color.red);
		Debug.DrawRay (l_forwardStartT, forwardDirection * distanceFront, Color.blue);
		Debug.DrawRay (l_forwardStartB, forwardDirection * distanceFront, Color.blue);
		
		//Debug.DrawRay (topSensorTransform.position, forwardDirection * distanceFront, Color.yellow);
		//Debug.DrawRay (bottomSensorTransform.position, forwardDirection * distanceFront, Color.yellow);
		
		// Create a raycast for each of the 3 sensor points
		RaycastHit2D hitR = Physics2D.Raycast(rightSensorTransform.position, downDirection, distanceSide, groundCheckMask);
		RaycastHit2D hitL = Physics2D.Raycast(leftSensorTransform.position, downDirection, distanceSide, groundCheckMask);
		RaycastHit2D hitG = Physics2D.Raycast(groundSensorTransform.position, downDirection, distanceGround, groundCheckMask);
		RaycastHit2D hitFT = Physics2D.Raycast(l_forwardStartT, forwardDirection, distanceFront, groundCheckMask);
		RaycastHit2D hitFB = Physics2D.Raycast(l_forwardStartB, forwardDirection, distanceFront, groundCheckMask);
		//RaycastHit2D hitT = Physics2D.Raycast(topSensorTransform.position, forwardDirection, distanceFront, groundCheckMask);
		//RaycastHit2D hitB = Physics2D.Raycast(bottomSensorTransform.position, forwardDirection, distanceFront, groundCheckMask);
		
		isHitR = false;
		isHitL = false;
		isHitG = false;
		isHitForward = false;
		
		hitPointR = Vector2.zero;
		hitPointL = Vector2.zero;
		hitPointG = Vector2.zero;
		hitPointForward = Vector2.zero;
		
		// The right sensor hit something
		if (hitR != null && hitR.collider != null)
		{
			isHitR = true;
			hitPointR = hitR.point;
		}
		
		// The left sensor hit something
		if (hitL != null && hitL.collider != null)
		{
			isHitL = true;
			hitPointL = hitL.point;
		}
		
		// The middle sensor hit something
		if (hitG != null && hitG.collider != null)
		{
			isHitG = true;
			hitPointG = hitG.point;
		}

		// The front top sensor hit something
		if (hitFT != null && hitFT.collider != null)
		{
			isHitForward = true;
			hitPointForward = hitFT.point;
		}

		// The front bottom sensor hit something
		if (hitFB != null && hitFB.collider != null)
		{
			isHitForward = true;
			hitPointForward = hitFB.point;
		}
	}

	void Attack()
	{
		attackHitbox.SetActive(true);
	}
}
using UnityEngine;
using System.Collections;

public class TopDownPlayerMovement : MonoBehaviour 
{
	public Animator bodyAnimator;
	public Animator headAnimator;
	public float speed;

	private Transform _transform;
	private Rigidbody2D _rigidbody2D;
	
	private FacingDirection _currentDirection;
	private bool _wasIdle;
	
	private enum FacingDirection
	{
		None,
		Up,
		Down,
		Left,
		Right
	}

	void Awake () 
	{
		_transform = transform;
		_rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	void OnEnable () 
	{
		SoftPauseScript.instance.SoftUpdate += SoftUpdate;
		_currentDirection = FacingDirection.Down;
	}

	void OnDisable () 
	{
		SoftPauseScript.instance.SoftUpdate -= SoftUpdate;
	}

	// Update is called once per frame
	void SoftUpdate (GameObject dispatcher) 
	{
		if (Input.GetAxis ("Horizontal") == 0 && Input.GetAxis ("Vertical") == 0) 
		{
			_rigidbody2D.velocity = Vector3.zero;
			if (!_wasIdle)
			{
				switch (_currentDirection)
				{
					case FacingDirection.Up:
						{
							SetAnimatorTriggers ("idleUp");
							break;
						}
					case FacingDirection.Down:
						{
							SetAnimatorTriggers ("idleDown");	
							break;
						}
					case FacingDirection.Left:
						{
							SetAnimatorTriggers ("idleLeft");
							break;				
						}
					case FacingDirection.Right:
						{
							SetAnimatorTriggers ("idleRight");	
							break;
						}
				}
			}
			_wasIdle = true;
			return;
		}

		MovePlayer ();
		UpdateAnimation ();
	}

	private void MovePlayer ()
	{
		Vector3 l_newVelocity = Vector3.zero;

		l_newVelocity.x += Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		l_newVelocity.y += Input.GetAxis ("Vertical") * speed * Time.deltaTime;

		_rigidbody2D.velocity = l_newVelocity;
	}

	private void UpdateAnimation ()
	{
		// Only update the animator if we have a new direction
		FacingDirection l_newDirection = FacingDirection.None;
		
		// Moving right
		if (Input.GetAxis ("Horizontal") > 0)
		{
			l_newDirection = FacingDirection.Right;
		} else if (Input.GetAxis("Horizontal") < 0)
		{
			l_newDirection = FacingDirection.Left;
		}

		// Moving up
		if (Input.GetAxis ("Vertical") > 0)
		{
			l_newDirection = FacingDirection.Up;
		} else if (Input.GetAxis ("Vertical") < 0)
		{
			l_newDirection = FacingDirection.Down;
		}
		if ((l_newDirection != _currentDirection && l_newDirection != FacingDirection.None) || _wasIdle)
		{
			_currentDirection = l_newDirection;
			_wasIdle = false;
			switch (_currentDirection)
			{
				case FacingDirection.Up:
				{
					SetAnimatorTriggers ("walkUp");
					break;
				}
				case FacingDirection.Down:
				{
					SetAnimatorTriggers ("walkDown");	
					break;
				}
				case FacingDirection.Left:
				{
					SetAnimatorTriggers("walkLeft");
					break;				
				}
				case FacingDirection.Right:
				{
					SetAnimatorTriggers("walkRight");	
					break;
				}
			}
		}
	}
	
	private void SetAnimatorTriggers(string p_trigger)
	{
		bodyAnimator.SetTrigger (p_trigger);
		headAnimator.SetTrigger (p_trigger);
	}
}

using UnityEngine;
using System.Collections;

public class SetPlayerStateScript : MonoBehaviour 
{
	private Transform myTransform;
//	public tk2dSpriteAnimator myAnimator;
	public Animator myAnimator;

	void Awake()
	{
		myTransform = transform;
	}

	void OnEnable()
	{
		Initialize();
		SoftPauseScript.instance.SoftUpdate += SoftUpdate;
	}

	public void Initialize() 
	{
		// Set up event listeners for state changes
		PlayerStateManager.instance.ChangeGroundState += ChangeGroundState;
		PlayerStateManager.instance.ChangeAttackState += ChangeAttackState;
		PlayerStateManager.instance.ChangeStunnedState += ChangeStunnedState;
		PlayerStateManager.instance.ChangeMoving += ChangeMoving;
	}

	void OnDisable()
	{
		SoftPauseScript.instance.SoftUpdate -= SoftUpdate;

		PlayerStateManager.instance.ChangeGroundState -= ChangeGroundState;
		PlayerStateManager.instance.ChangeAttackState -= ChangeAttackState;
		PlayerStateManager.instance.ChangeStunnedState -= ChangeStunnedState;
		PlayerStateManager.instance.ChangeMoving -= ChangeMoving;
	}

	void SoftUpdate(GameObject dispatcher)
	{
		// We will probably need to check some things here
	}

	private void ChangeGroundState(GameObject dispatcher)
	{
		switch (PlayerStateManager.instance.currentGroundState)
		{
			case Enums.PlayerGroundState.OnGround:
			{
				if (PlayerStateManager.instance.isMoving)
				{
					myAnimator.SetTrigger ("run");
//					myAnimator.Play("run");
				}
				else if (PlayerStateManager.instance.currentAttackState == Enums.PlayerAttackState.None)
				{
//					myAnimator.Play("idle");
					myAnimator.SetTrigger ("idle");
				}
				break;
			}
			case Enums.PlayerGroundState.Rising:
			{
//				myAnimator.Play("jump");
				myAnimator.SetTrigger ("jump");
				break;
			}
			case Enums.PlayerGroundState.Falling:
			{
//				myAnimator.Play("fall");
				myAnimator.SetTrigger ("fall");
				break;
			}
			case Enums.PlayerGroundState.Landing:
			{
				if (PlayerStateManager.instance.currentAttackState == Enums.PlayerAttackState.None)
				{
//					myAnimator.Play("land");

					// If you feel fancy add a landing animation
				}
				break;
			}
		}
	}

	private void ChangeAttackState(GameObject dispatcher)
	{
		if (PlayerStateManager.instance.currentGroundState == Enums.PlayerGroundState.Rising || 
		    PlayerStateManager.instance.currentGroundState == Enums.PlayerGroundState.Falling)
		{
			switch (PlayerStateManager.instance.currentAttackState)
			{
				// Do air attacks
			}
		}
		else
		{
			switch (PlayerStateManager.instance.currentAttackState)
			{
				// Do ground attacks
			}
		}
	}

	private void ChangeStunnedState(GameObject dispatcher)
	{
		switch (PlayerStateManager.instance.currentStunnedState)
		{
			case Enums.PlayerStunnedState.Hit:
			{
//				myAnimator.Play("hit");
				myAnimator.SetTrigger ("hit");
				break;
			}
		}
	}

	private void ChangeMoving(GameObject dispatcher)
	{
		if (PlayerStateManager.instance.isMoving)
		{
			if (PlayerStateManager.instance.currentGroundState == Enums.PlayerGroundState.OnGround)
			{
//				myAnimator.Play("run");
				myAnimator.SetTrigger ("run");
			}
		}
		else
		{
			if (PlayerStateManager.instance.currentGroundState == Enums.PlayerGroundState.OnGround)
			{
//				myAnimator.Play("idle");
				myAnimator.SetTrigger ("idle");
			}
		}
	}
}
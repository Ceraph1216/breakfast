using UnityEngine;
using System.Collections;

public class SetPlayerStateScript : MonoBehaviour 
{
	private Transform myTransform;

	public Animator headAnimator;
	public Animator bodyAnimator;

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
					SetAnimatorTriggers("run");
				}
				else if (PlayerStateManager.instance.currentAttackState == Enums.PlayerAttackState.None)
				{
					SetAnimatorTriggers("idle");
				}
				break;
			}
			case Enums.PlayerGroundState.Rising:
			{
				SetAnimatorTriggers("jump");
				break;
			}
			case Enums.PlayerGroundState.Falling:
			{
				SetAnimatorTriggers("fall");
				break;
			}
			case Enums.PlayerGroundState.Landing:
			{
				if (PlayerStateManager.instance.currentAttackState == Enums.PlayerAttackState.None)
				{
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
				SetAnimatorTriggers("hit");
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
				SetAnimatorTriggers("run");
			}
		}
		else
		{
			if (PlayerStateManager.instance.currentGroundState == Enums.PlayerGroundState.OnGround)
			{
				SetAnimatorTriggers("idle");
			}
		}
	}
	
	private void SetAnimatorTriggers(string p_trigger)
	{
		bodyAnimator.SetTrigger (p_trigger);
		headAnimator.SetTrigger (p_trigger);
	}
}
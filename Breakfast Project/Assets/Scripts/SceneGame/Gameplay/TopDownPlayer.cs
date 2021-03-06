﻿using UnityEngine;
using System.Collections;

public class TopDownPlayer : MonoBehaviour 
{
	public RuntimeAnimatorController messyHeadController;
	public RuntimeAnimatorController hatHeadController;
	public RuntimeAnimatorController pjBodyController;
	public RuntimeAnimatorController normalBodyController;
	
	public Animator headAnimator;
	public Animator bodyAnimator;
	
	public Action currentAction;
	public bool canInteract;
	
	private TopDownPlayerMovement _movement;
	private Rigidbody2D _rigidbody2D;
	
	void Awake ()
	{
		_movement = GetComponent<TopDownPlayerMovement> ();	
	}
	
	void OnEnable () 
	{
		SoftPauseScript.instance.SoftUpdate += SoftUpdate;
		canInteract = true;
		CheckAnimators ();
	}

	void OnDisable () 
	{
		SoftPauseScript.instance.SoftUpdate -= SoftUpdate;
	}

	// Update is called once per frame
	void SoftUpdate (GameObject dispatcher)
	{
		if (!canInteract)
		{
			return;	
		}
		
		if (Input.GetButtonDown ("Submit"))
		{
			if (currentAction != null)
			{
				canInteract = false;
				currentAction.DoAction ();
			}
		}
	}
	
	public void FreezePlayer ()
	{
		_movement.StopMovement ();
	}

	public void UnfreezePlayer ()
	{
		_movement.StartMovement ();	
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

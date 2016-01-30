using UnityEngine;
using System.Collections;

public class TopDownPlayer : MonoBehaviour 
{
	public RuntimeAnimatorController messyHeadController;
	public RuntimeAnimatorController hatHeadController;
	public RuntimeAnimatorController pjBodyController;
	public RuntimeAnimatorController normalBodyController;
	
	public Animator headAnimator;
	public Animator bodyAnimator;
	
	public Interactable currentInteractable;
	public bool canInteract;
	
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
		
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.E))
		{
			if (currentInteractable != null)
			{
				currentInteractable.DoAction ();
				canInteract = false;
			}
		}
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

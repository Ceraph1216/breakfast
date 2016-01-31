using UnityEngine;
using System.Collections;

public class SetupHouse : MonoBehaviour 
{
	public Transform bedLocation;
	public Transform outOfBedLocation;
	public Transform alarmLocation;
	public Transform hatLocation;
	public Transform clothesLocation;
	public Transform bathroomLocation;
	public Transform fridgeLocation;
	public Transform tableLocation;
	
	public GameObject fadeIn;
	public GameObject firstFade;
	
	public GameObject alarmRinging;
	public GameObject alarmBroken;
	public GameObject hat;
	public GameObject closetOpen;
	public GameObject closetClosed;
	public GameObject dentalHygene;
	public GameObject fridgeOpen;
	public GameObject fridgeClosed;
	public GameObject cerealNoMilk;
	public GameObject cerealMilk;
	public GameObject cerealMilkPoured;
	
	public GameObject getOutOfBed;
	public GameObject closetBox;
	public GameObject AlarmBox;
	public GameObject bathroomBox;
	public GameObject fridgeBox;
	
	private Transform _playerTransform;
	private Transform _cameraTransform;
	
	void Awake ()
	{
		_playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;	
		_cameraTransform = Camera.main.transform;
	}
	
	void OnEnable ()
	{
		SetPlayerPosition ();
		
		if (GameplayManager.instance.gotUp)
		{
			fadeIn.SetActive(true);
		}
		else
		{
			firstFade.SetActive(true);
			fadeIn.SetActive(false);
		}
		
		if (GameplayManager.instance.hasHat)
		{
			hat.SetActive(false);
		}
		
		if (GameplayManager.instance.hasClothes)
		{
			closetClosed.SetActive(false);
			closetOpen.SetActive(true);
			closetBox.SetActive(false);
		}
		else
		{
			closetClosed.SetActive(true);
			closetOpen.SetActive(false);
			closetBox.SetActive(true);
		}
		
		if (GameplayManager.instance.stoppedAlarm)
		{
			alarmRinging.SetActive(false);
			alarmBroken.SetActive(true);
			AlarmBox.SetActive(false);
		}
		else
		{
			alarmRinging.SetActive(true);
			alarmBroken.SetActive(false);
			AlarmBox.SetActive(true);
		}
		
		if (GameplayManager.instance.brushedTeeth)
		{
			dentalHygene.SetActive(false);
			bathroomBox.SetActive(false);
		}
		else
		{
			dentalHygene.SetActive(true);
			bathroomBox.SetActive(true);
		}
		
		if (GameplayManager.instance.hasMilk)
		{
			fridgeClosed.SetActive(false);
			fridgeOpen.SetActive(true);
			cerealMilk.SetActive(true);
			cerealNoMilk.SetActive(false);
			cerealMilkPoured.SetActive(false);
		}
		else
		{
			fridgeClosed.SetActive(true);
			fridgeOpen.SetActive(false);
			cerealMilk.SetActive(false);
			cerealNoMilk.SetActive(true);
			cerealMilkPoured.SetActive(false);
		}
		
		if (GameplayManager.instance.pouredCereal)
		{
			cerealMilk.SetActive(false);
			cerealNoMilk.SetActive(false);
			cerealMilkPoured.SetActive(true);
		}
	}
	
	private void SetPlayerPosition()
	{
		Vector3 l_newPosition = bedLocation.position;
		switch (GameplayManager.instance.lastLevelbeaten)
		{
			
			case "getUp":
			{
				l_newPosition = outOfBedLocation.position;
				break;	
			}
			case "alarm":
			{
				l_newPosition = alarmLocation.position;
				break;	
			}
			case "hat":
			{
				l_newPosition = hatLocation.position;
				break;	
			}
			case "clothes":
			{
				l_newPosition = clothesLocation.position;
				break;	
			}
			case "bathroom":
			{
				l_newPosition = bathroomLocation.position;
				break;	
			}
			case "fridge":
			{
				l_newPosition = fridgeLocation.position;
				break;	
			}
			case "pourCereal":
			{
				l_newPosition = tableLocation.position;
				break;	
			}
			default:
			{
				l_newPosition = bedLocation.position;
				break;
			}
		}
		
		_playerTransform.position = l_newPosition;
		_cameraTransform.position = l_newPosition + new Vector3(0,0,-10);
	}
}

using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour 
{
	static GameplayManager mInstance;

	// The instance of the GameplayManager class. Will create it if one isn't already around.

	static public GameplayManager instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = Object.FindObjectOfType(typeof(GameplayManager)) as GameplayManager;

				if (mInstance == null)
				{
					GameObject go = new GameObject("_GameplayManager");
					DontDestroyOnLoad(go);
					mInstance = go.AddComponent<GameplayManager>();
				}
			}
			return mInstance;
		}
	}
	
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);	
	}
	
	public bool hasHat;
	public bool hasClothes;
	public bool stoppedAlarm;
	public bool brushedTeeth;
	public bool openedDoor;
	public bool hasCereal;
	public bool pouredCereal;
	
	public string levelToLoad;
}

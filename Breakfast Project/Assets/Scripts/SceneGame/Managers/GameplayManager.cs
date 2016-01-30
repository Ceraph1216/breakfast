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
	
	public bool hasHat;
	public bool hasClothes;
	public bool brushedTeeth;
	public bool openedDoor;
	public bool hasMilk;
	public bool hasCereal;
	public bool pouredCereal;
}

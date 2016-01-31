using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour 
{
	static AudioManager mInstance;

	// Whether there is an instance of the AudioManager class present.

	static public bool isActive { get { return mInstance != null; } }

	// The instance of the AudioManager class. Will create it if one isn't already around.

	static public AudioManager instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = Object.FindObjectOfType(typeof(AudioManager)) as AudioManager;

				if (mInstance == null)
				{
					GameObject go = new GameObject("_AudioManager");
					DontDestroyOnLoad(go);
					mInstance = go.AddComponent<AudioManager>();
				}
			}
			return mInstance;
		}
	}
	
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);	
	}
}

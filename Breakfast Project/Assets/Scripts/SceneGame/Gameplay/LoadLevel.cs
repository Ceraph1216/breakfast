using UnityEngine;
using System.Collections;

public class LoadLevel : Interactable 
{
	public string levelToLoad;
	
	public override void DoAction ()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (levelToLoad);
	}
}

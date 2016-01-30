using UnityEngine;
using System.Collections;

public class LoadLevel : Action 
{
	public string levelToLoad;
	
	public override void DoAction ()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (levelToLoad);
	}
}

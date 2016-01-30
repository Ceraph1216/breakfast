using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour 
{
	public void OnClick ()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("DreamScene");	
	}
}

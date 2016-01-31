using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour 
{
	public void OnClick ()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("DreamScene");	
	}
	
	void Update ()
	{
		if (Input.GetButtonDown ("Submit"))
		{
			OnClick ();
		}
	}
}

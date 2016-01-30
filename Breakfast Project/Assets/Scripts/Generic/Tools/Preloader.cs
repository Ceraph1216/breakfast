using UnityEngine;
using System.Collections;

public class Preloader : MonoBehaviour 
{
	void OnEnable ()
	{
		GameplayManager.instance.hasHat = true;
		GameplayManager.instance.hasClothes = true;
		
		UnityEngine.SceneManagement.SceneManager.LoadScene ("TitleScreen");
	}
}

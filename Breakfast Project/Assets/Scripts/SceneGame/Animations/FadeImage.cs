using UnityEngine;
using System.Collections;

public class FadeImage : MonoBehaviour 
{
	public bool fadeIn;
	public float duration;
	public Action[] actionsOnComplete;
	
	private UnityEngine.UI.Image image;
	private float _currentTime;
	
	void Awake ()
	{
		image = GetComponent<UnityEngine.UI.Image> ();
	}
	
	void OnEnable ()
	{
		_currentTime = 0;
	}

	
	// Update is called once per frame
	void Update () 
	{
		if (_currentTime >= duration)
		{
			OnComplete ();
			return;
		}
		
		float l_percentComplete = _currentTime / duration;
		Color l_newColor = image.color;
		if (fadeIn)
		{
			l_newColor.a = Mathf.Lerp (0, 1, l_percentComplete);
		} else
		{
			l_newColor.a = Mathf.Lerp (1, 0, l_percentComplete);
		}
		
		image.color = l_newColor;
		
		_currentTime += Time.deltaTime;
	}
	
	private void OnComplete ()
	{
		for (int i = 0; i < actionsOnComplete.Length; i++)
		{
			actionsOnComplete [i].DoAction ();
		}
		
		enabled = false;
	}
}

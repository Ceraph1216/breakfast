using UnityEngine;
using System.Collections;

public class TextManager : MonoBehaviour 
{
	static TextManager mInstance;

	// The instance of the TextManager class. Will create it if one isn't already around.

	static public TextManager instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = Object.FindObjectOfType(typeof(TextManager)) as TextManager;

				if (mInstance == null)
				{
					GameObject go = new GameObject("_TextManager");
					DontDestroyOnLoad(go);
					mInstance = go.AddComponent<TextManager>();
				}
			}
			return mInstance;
		}
	}
	
	private UILabel _label;
	public UILabel label
	{
		get
		{
			if (_label == null)
			{
				_label = GameObject.FindGameObjectWithTag ("textBox").GetComponent<UILabel>();	
			}
			
			return _label;
		}
	}
	
	public void SetText(string p_newText)
	{
		label.text = p_newText;
		label.gameObject.AddComponent<TypewriterEffect> ();
	}
	
	public void ClearText ()
	{
		TypewriterEffect l_typeWriter = label.gameObject.GetComponent<TypewriterEffect> ();
		
		if (l_typeWriter != null)
		{
			Destroy (l_typeWriter);
		}
		
		label.text = "";	
	}
}

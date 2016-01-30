using UnityEngine;
using System.Collections;

public class SetFontFilterMode : MonoBehaviour 
{
	public Font font;
	public FilterMode filterMode;
	
	void OnEnable ()
	{
		font.material.mainTexture.filterMode = filterMode;
	}
}

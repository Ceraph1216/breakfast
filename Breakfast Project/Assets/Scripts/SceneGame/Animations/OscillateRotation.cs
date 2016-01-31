using UnityEngine;
using System.Collections;

public class OscillateRotation : MonoBehaviour 
{
	public float speed;
	public Vector3 startRotation;
	public Vector3 endRotation;
	
	private Quaternion _startQuaternion;
	private Quaternion _endQuaternion;
	
	private Transform _transform;
	
	private bool _forward;
	private float _currentPercent;
	private float _currentIncrease;
	
	void Awake ()
	{
		_transform = transform;	
	}
	
	void OnEnable () 
	{
		_startQuaternion = Quaternion.Euler (startRotation);
		_endQuaternion = Quaternion.Euler (endRotation);
		_forward = true;
		_currentPercent = 0;
		_currentIncrease = 0;
		
		SoftPauseScript.instance.SoftUpdate += SoftUpdate;
	}

	void OnDisable () 
	{
		SoftPauseScript.instance.SoftUpdate -= SoftUpdate;
	}

	// Update is called once per frame
	void SoftUpdate (GameObject dispatcher)
	{
		if (_currentPercent >= 1)
		{
			_currentIncrease = 0;
			_currentPercent = 0;
			_forward = !_forward;
			
			if (_forward)
			{
				_transform.localRotation = _startQuaternion;
			} else
			{
				_transform.localRotation = _endQuaternion;
			}
			return;
		}
		
		if (_currentPercent < 0.5f)
		{
			_currentIncrease += speed * Time.deltaTime;
		} else
		{
			_currentIncrease -= speed * Time.deltaTime;
		}
		
		_currentPercent += _currentIncrease;
		
		Quaternion l_newRotation = Quaternion.identity;
		
		if (_forward)
		{
			l_newRotation = Quaternion.Slerp (_startQuaternion, _endQuaternion, _currentPercent);
		} 
		else
		{
			l_newRotation = Quaternion.Slerp (_endQuaternion, _startQuaternion, _currentPercent);
		}
		
		_transform.localRotation = l_newRotation;
	}
}

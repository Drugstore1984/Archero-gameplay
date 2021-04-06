using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	[SerializeField] GameObject _joystick;
	public void PauseGame()
	{
		if(Time.timeScale == 1)
        {
			Time.timeScale = 0f;
			_joystick.SetActive(false);
			Debug.Log("Paused");
		}
		else if(Time.timeScale == 0)
        {
			Time.timeScale = 1f;
			_joystick.SetActive(true);
			Debug.Log("Resume");
		}
		
	}
}

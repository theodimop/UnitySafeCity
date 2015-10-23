using UnityEngine;
using System.Collections;

public class pauseScript : MonoBehaviour {

	
	//For knowing when game is paused
	public bool isPaused;



	// Use this for initialization
	void Start () {
		isPaused = false;
	}

	void Update()
	{
		//button listener for pause
		if (Input.GetKeyUp (KeyCode.Space)) 
		{
			isPaused = !isPaused;
		}

		//freeze time ...
		if (isPaused)
		{
			Time.timeScale = 0;
			AudioListener.pause=true;
		}
		else if (!isPaused)
		{
			Time.timeScale = 1;
			AudioListener.pause=false;
		}
	}


}

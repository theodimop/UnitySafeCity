using UnityEngine;
using System.Collections;

public class StageObserveInitializer : MonoBehaviour {

    //Main car
    public GameObject vehiclePlayer;

    //Player's level
    private int level;

    //Use like counter , means 3 seconds
    private bool boolean3 = false;


    void Awake()
    {
       
    }

	// Use this for initialization
	void Start () {


        
        if (PlayerPrefs.HasKey ("level")) {
			level = PlayerPrefs.GetInt ("level");
			//Set the starting posittion of main vehicle 
			switch(level)
			{
			case 1:
                vehiclePlayer.transform.position = new Vector3(10f, 1f, 10f);
                break;
			case 2:
				vehiclePlayer.transform.position = new Vector3(250f,1f,35f);
				break;
			case 3:
				break;
			case 4:
				break;
			case 5:
				break;
			case 6:
				break;
			case 7:
				break;
			case 8:
				break;
			case 9:
				break;
			case 10:
				break;
			}

		} else
		{
			PlayerPrefs.SetInt("level",1);
		}


		StartCoroutine (Wait (3f));


	}

	IEnumerator Wait(float secs)
	{
		yield return new WaitForSeconds	(secs);
		boolean3 = true;


	}

	// Update is called once per frame
	void Update () {
		if (boolean3)
		{
			Debug.Log ("3 secs passed");
			boolean3= false;
		}
	}
}

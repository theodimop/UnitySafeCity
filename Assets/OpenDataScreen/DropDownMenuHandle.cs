using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DropDownMenuHandle : MonoBehaviour {



    

    public Text textCounty,textDay,textMonth,textYear,textAccidentType;    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Listener for button Show me Data
    public void buttonListenerShowMeData()
    {

        Debug.Log(textCounty.text + " " + textDay.text + "" + textMonth.text + "" + textYear.text+""+textAccidentType.text);        
    }
}

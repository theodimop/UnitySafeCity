using UnityEngine;
using System.Collections;

public class ButtonStart : MonoBehaviour {

    private int currentlevelDisplayed;
    private string currentStageDisplayed;


    //BUtton start Listener
    public void buttonStartOnCLick()
    {
        


        PlayerPrefs.GetInt("currentlevel");
        PlayerPrefs.GetString("stagebuttonpressed");
    }
}

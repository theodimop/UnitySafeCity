using UnityEngine;
using System.Collections;

public class AfterLoginScript : MonoBehaviour {

    //get panel
    public GameObject panelLocked;

    public void unLockLevel()
    {
        panelLocked.SetActive(false);

        int oldScore = PlayerPrefs.GetInt("score");
        PlayerPrefs.SetInt("score",oldScore+10);
    }


}

using UnityEngine;
using System.Collections;
/*
 For this class, it's important to use playeyprefs for displaying the window,
 */

public class PlayerPrefsInit : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        //Create a playerPref for keeping score
        if (!PlayerPrefs.HasKey("score"))
            PlayerPrefs.SetInt("score", 0);

        //Create a playerPref for keeping level of player
        if (!PlayerPrefs.HasKey("level"))
            PlayerPrefs.SetInt("level", 1);

        //Create a playerPref for keeping level of player
        if (!PlayerPrefs.HasKey("stage"))
            PlayerPrefs.SetString("stage", "quiz");



        //Create a playerPref for level that player is viewing in window
        if (!PlayerPrefs.HasKey("currentlevel"))
            PlayerPrefs.SetInt("currentlevel",1);


        //Create a playerPref for stage that player is viewing in window
        if (!PlayerPrefs.HasKey("currentstage"))
            PlayerPrefs.SetString("stage", "quiz");


        //** MPORW NA TO GLITWSW AN TO LEVEL TO KANW STRING ME TITLO
        //Create playerPrefs for levelTitles
        //Check only the first because all are generated together
        if (!PlayerPrefs.HasKey("leveltitle1"))
        {
            PlayerPrefs.SetString("leveltitle1", "LEARN THE BASICS");
            PlayerPrefs.SetString("leveltitle2", "Level 2");
            PlayerPrefs.SetString("leveltitle3", "LEARN THE BASICS");
            PlayerPrefs.SetString("leveltitle4", "LEARN THE BASICS");
            PlayerPrefs.SetString("leveltitle5", "LEARN THE BASICS");
            PlayerPrefs.SetString("leveltitle6", "LEARN THE BASICS");
            PlayerPrefs.SetString("leveltitle7", "LEARN THE BASICS");
            PlayerPrefs.SetString("leveltitle8", "LEARN THE BASICS");
            PlayerPrefs.SetString("leveltitle9", "LEARN THE BASICS");
            PlayerPrefs.SetString("leveltitle10", "LEARN THE BASICS");
        }
        //Create playerPrefs for level Description
        // TODO: ds
        if (!PlayerPrefs.HasKey("leveldescription1"))
        {
            PlayerPrefs.SetString("leveldescription1", "This is a demo description");
            PlayerPrefs.SetString("leveldescription2", "lvl2description");
            PlayerPrefs.SetString("leveldescription3", "lvl3description");
            PlayerPrefs.SetString("leveldescription4", "lvl4description");
            PlayerPrefs.SetString("leveldescription5", "lvl5description");
            PlayerPrefs.SetString("leveldescription6", "lvl6description");
            PlayerPrefs.SetString("leveldescription7", "lvl7description");
            PlayerPrefs.SetString("leveldescription8", "lvl8description");
            PlayerPrefs.SetString("leveldescription9", "lvl9descriptionS");
            PlayerPrefs.SetString("leveldescription10", "lvl10description");
        }


        //Create playerPrefs for Level Score
        if (!PlayerPrefs.HasKey("levelScore1"))
        {
            PlayerPrefs.SetInt("levelscore1", 00111);
            PlayerPrefs.SetInt("levelscore2", 100112);
            PlayerPrefs.SetInt("levelscore3", 00111);
            PlayerPrefs.SetInt("levelscore4", 00111);
            PlayerPrefs.SetInt("levelscore5", 00111);
            PlayerPrefs.SetInt("levelscore6", 00111);
            PlayerPrefs.SetInt("levelscore7", 00111);
            PlayerPrefs.SetInt("levelscore8", 00111);
            PlayerPrefs.SetInt("levelscore9", 00111);
            PlayerPrefs.SetInt("levelscore10", 00111);
        }
     


        //Create playerPrefs for stage Scores
        if (!PlayerPrefs.HasKey("stagescore1quiz"))
        {
            PlayerPrefs.SetInt("stagescore1quiz", 1800);
            PlayerPrefs.SetInt("stagescore1observe", 2440);
            PlayerPrefs.SetInt("stagescore1drive", 3000);
            PlayerPrefs.SetInt("stagescore2quiz", 00111);
            PlayerPrefs.SetInt("stagescore2observe", 100112);
            PlayerPrefs.SetInt("stagescore2drive", 00111);
            PlayerPrefs.SetInt("stagescore3quiz", 00111);
            PlayerPrefs.SetInt("stagescore3observe", 100112);
            PlayerPrefs.SetInt("stagescore3drive", 00111);
            PlayerPrefs.SetInt("stagescore4quiz", 00111);
            PlayerPrefs.SetInt("stagescore4observe", 100112);
            PlayerPrefs.SetInt("stagescore4drive", 00111);
            PlayerPrefs.SetInt("stagescore5quiz", 00111);
            PlayerPrefs.SetInt("stagescore5observe", 100112);
            PlayerPrefs.SetInt("stagescore5drive", 00111);
            PlayerPrefs.SetInt("stagescore6quiz", 00111);
            PlayerPrefs.SetInt("stagescore6observe", 100112);
            PlayerPrefs.SetInt("stagescore6drive", 00111);
            PlayerPrefs.SetInt("stagescore7quiz", 00111);
            PlayerPrefs.SetInt("stagescore7observe", 100112);
            PlayerPrefs.SetInt("stagescore7drive", 00111);
            PlayerPrefs.SetInt("stagescore8quiz", 00111);
            PlayerPrefs.SetInt("stagescore8observe", 100112);
            PlayerPrefs.SetInt("stagescore8drive", 00111);
            PlayerPrefs.SetInt("stagescore9quiz", 00111);
            PlayerPrefs.SetInt("stagescore9observe", 100112);
            PlayerPrefs.SetInt("stagescore9drive", 00111);
            PlayerPrefs.SetInt("stagescore10quiz", 00111);
            PlayerPrefs.SetInt("stagescore10observe", 100112);
            PlayerPrefs.SetInt("stagescore10drive", 00111);
      
        }
        //Create playerPrefs for Stage Description
        if(!PlayerPrefs.HasKey("stagedescription1quiz"))
        {
            PlayerPrefs.SetString("stagedescription1quiz","description 1 quiz");
            PlayerPrefs.SetString("stagedescription2quiz", "description 2 quiz");
            PlayerPrefs.SetString("stagedescription3quiz", "description 3 quiz");
            PlayerPrefs.SetString("stagedescription4quiz", "description 4 quiz");
            PlayerPrefs.SetString("stagedescription5quiz", "description 1 quiz");
            PlayerPrefs.SetString("stagedescription6quiz", "description 1 quiz");
            PlayerPrefs.SetString("stagedescription7quiz", "description 1 quiz");
            PlayerPrefs.SetString("stagedescription8quiz", "description 1 quiz");
            PlayerPrefs.SetString("stagedescription9quiz", "description 1 quiz");
            PlayerPrefs.SetString("stagedescription10quiz", "description 1 quiz");
            PlayerPrefs.SetString("stagedescription1observe", "description 1 observe");
            PlayerPrefs.SetString("stagedescription2observe", "description 2 observe");
            PlayerPrefs.SetString("stagedescription3observe", "description 3 observe");
            PlayerPrefs.SetString("stagedescription4observe", "description 4 observe");
            PlayerPrefs.SetString("stagedescription5observe", "description 1 observe");
            PlayerPrefs.SetString("stagedescription6observe", "description 1 observe");
            PlayerPrefs.SetString("stagedescription7observe", "description 1 observe");
            PlayerPrefs.SetString("stagedescription8observe", "description 1 observe");
            PlayerPrefs.SetString("stagedescription9observe", "description 1 observe");
            PlayerPrefs.SetString("stagedescription10observe", "description 1 observe");
            PlayerPrefs.SetString("stagedescription1drive", "description 1 drive");
            PlayerPrefs.SetString("stagedescription2drive", "description 2 drive");
            PlayerPrefs.SetString("stagedescription3drive", "description 3 drive");
            PlayerPrefs.SetString("stagedescription4drive", "description 4 drive");
            PlayerPrefs.SetString("stagedescription5drive", "description 1 drive");
            PlayerPrefs.SetString("stagedescription6drive", "description 1 drive");
            PlayerPrefs.SetString("stagedescription7drive", "description 1 drive");
            PlayerPrefs.SetString("stagedescription8drive", "description 1 drive");
            PlayerPrefs.SetString("stagedescription9drive", "description 1 drive");
            PlayerPrefs.SetString("stagedescription10drive", "description 1 drive");

        }

        //Probably some more.. in the future
	}
	

}

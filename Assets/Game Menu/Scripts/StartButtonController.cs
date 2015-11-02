using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class StartButtonController : MonoBehaviour {
    public Button startButton;
   

    //This update fuc allow user to play all stages an level in correct order.
    void Update()
    {
        
        if(PlayerPrefs.GetInt("level") >= getCurrentLevel())
        {
            switch(getCurrentStage())
            {
                case "quiz":
                    startButton.interactable = true;
                    break;
                case "observe":
                    if(PlayerPrefs.GetString("stage").Equals("quiz"))
                        startButton.interactable = false;
                    else
                        startButton.interactable = true;
                    break;
                case "drive":
                    if (!PlayerPrefs.GetString("stage").Equals("drive") )
                        startButton.interactable = false;
                    else
                        startButton.interactable = true;
                    break;
            }
        }
        else
            startButton.interactable = false;


    }

    public void startButtonOnClick()
    {
        startGame();
    }


    private void startGame()
    {
        string stage = getCurrentStage();
        switch(stage)
        {
            case "quiz"://start quiz
                Application.LoadLevel("QuizStageScene");
                break;
            case "observe"://start...
                break;
            case "drive"://TODO start..
                break;

        }
    }
    //Stage % LEvel getters
    private string getCurrentStage()
    {
        return PlayerPrefs.GetString("currentstage");
    }
    private int getCurrentLevel()
    {
        return PlayerPrefs.GetInt("currentlevel");
      
    }



}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class StageManager : MonoBehaviour {

    //Stage Button
    public Button buttonStageQuiz;
    public Button buttonStageObserve;
    public Button buttonStageDrive;

    //Level score with the steering icons
    public Text textStageScore;
    public Text textStageDescription;
    public Image imageStageScoreSteerLow;
    public Image imageStageScoreSteerMed;
    public Image imageStageScoreSteerHigh;

	// Use this for initialization
	void Start () {
        setStageButtonPressed("quiz");
	}


    //Stage button listeners

    public void buttonQuizPressed()
    {
        setCurrentStage("quiz");
        setStageButtonPressed("quiz");
    }
    public void buttonObservePressed()
    {
        setCurrentStage("observe");
        setStageButtonPressed("observe");
    }
    public void buttonDrivePressed()
    {
        setCurrentStage("drive");
        setStageButtonPressed("drive");
    }


    //Control the Stages buttons 
    private void setStageButtonPressed(string stage)
    {
           //check which button lastly pressed
        switch (stage)
        {
            case "quiz":
                buttonStageQuiz.interactable = false;
                buttonStageObserve.interactable = true;
                buttonStageDrive.interactable = true;
                setCurrentStage("quiz");
                setStageScore_Description("quiz");
                break;
            case "observe":
                buttonStageObserve.interactable = false;
                buttonStageQuiz.interactable = true;
                buttonStageDrive.interactable = true;
                setCurrentStage("observe");
                setStageScore_Description("observe");
                break;
            case "drive":
                buttonStageDrive.interactable = false;
                buttonStageQuiz.interactable = true;
                buttonStageObserve.interactable = true;
                setCurrentStage("drive");
                setStageScore_Description("drive");
                break;
            default:
                buttonStageQuiz.enabled=false;
                buttonStageObserve.interactable = true;
                buttonStageDrive.interactable = true;
                PlayerPrefs.SetString("currentstage", "observer");///here correction
                break;
        }
    }


    //
    private void setStageScore_Description(string curStage)
    {
        int score=0;
       switch(PlayerPrefs.GetInt("currentlevel"))
       {
           case 1:
               switch (curStage)
               {
                   case "quiz" :
                      score =  PlayerPrefs.GetInt("stagescore1quiz");
                      textStageScore.text= Convert.ToString( score);
                      textStageDescription.text = PlayerPrefs.GetString("stagedescription1quiz");
                      setScoreOnSteerIconsQuiz(score);
                       break;
                   case "observe":
                       score = PlayerPrefs.GetInt("stagescore1observe");
                       textStageScore.text = Convert.ToString(score);
                       textStageDescription.text = PlayerPrefs.GetString("stagedescription1observe");
                       setScoreOnSteerIconsObserve(score);
                       break;
                   case "drive":
                       score = PlayerPrefs.GetInt("stagescore1drive");
                       textStageScore.text = Convert.ToString(score);
                       textStageDescription.text = PlayerPrefs.GetString("stagedescription1drive");
                       setScoreOnSteerIconsDrive(score);
                       break;
               }
               break;
           case 2:
               switch (curStage)
               {
                   case "quiz":
                       score = PlayerPrefs.GetInt("stagescore2quiz");
                       textStageScore.text = Convert.ToString(score);
                       textStageDescription.text = PlayerPrefs.GetString("stagedescription2quiz");
                       setScoreOnSteerIconsQuiz(score);
                       break;
                   case "observe":
                       score = PlayerPrefs.GetInt("stagescore2observe");
                       textStageScore.text = Convert.ToString(score);
                       textStageDescription.text = PlayerPrefs.GetString("stagedescription2observe");
                       setScoreOnSteerIconsObserve(score);
                       break;
                   case "drive":
                       score = PlayerPrefs.GetInt("stagescore2drive");
                       textStageScore.text = Convert.ToString(score);
                       textStageDescription.text = PlayerPrefs.GetString("stagedescription2drive");
                       setScoreOnSteerIconsDrive(score);
                       break;
               }
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
    }

    private void setScoreOnSteerIconsQuiz(int score)
    {
        if (score < 1500)
        {
            imageStageScoreSteerLow.color = new Color(0, 0, 0, 0);
            imageStageScoreSteerMed.color = new Color(0, 0, 0,0);
            imageStageScoreSteerHigh.color = new Color(0, 0, 0, 0);
        }
        else if (score < 2000)
        {
            imageStageScoreSteerLow.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerMed.color = new Color(0, 0, 0,0);
            imageStageScoreSteerHigh.color = new Color(0, 0, 0, 0);
        }
        else if (score < 2500)
        {
            imageStageScoreSteerLow.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerMed.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerHigh.color = new Color(0, 0, 0, 0);
        }
        else
        {
            imageStageScoreSteerLow.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerMed.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerHigh.color = new Color(253, 253, 0, 253);
        }


    }

    private void setScoreOnSteerIconsObserve(int score)
    {
        if (score < 1500)
        {
            imageStageScoreSteerLow.color = new Color(0, 0, 0, 0);
            imageStageScoreSteerMed.color = new Color(0, 0, 0, 0);
            imageStageScoreSteerHigh.color = new Color(0, 0, 0, 0);
        }
        else if (score < 2000)
        {
            imageStageScoreSteerLow.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerMed.color = new Color(0, 0, 0, 0);
            imageStageScoreSteerHigh.color = new Color(0, 0, 0, 0);
        }
        else if (score < 2500)
        {
            imageStageScoreSteerLow.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerMed.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerHigh.color = new Color(0, 0, 0, 0);
        }
        else
        {
            imageStageScoreSteerLow.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerMed.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerHigh.color = new Color(253, 253, 0, 253);
        }
    }

    private void setScoreOnSteerIconsDrive(int score)
    {
        if (score < 1500)
        {
            imageStageScoreSteerLow.color = new Color(0, 0, 0, 0);
            imageStageScoreSteerMed.color = new Color(0, 0, 0, 0);
            imageStageScoreSteerHigh.color = new Color(0, 0, 0, 0);
        }
        else if (score < 2000)
        {
            imageStageScoreSteerLow.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerMed.color = new Color(0, 0, 0, 0);
            imageStageScoreSteerHigh.color = new Color(0, 0, 0, 0);
        }
        else if (score < 2500)
        {
            imageStageScoreSteerLow.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerMed.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerHigh.color = new Color(0, 0, 0, 0);
        }
        else
        {
            imageStageScoreSteerLow.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerMed.color = new Color(253, 253, 0, 253);
            imageStageScoreSteerHigh.color = new Color(253, 253, 0, 253);
        }
    }


    //Stage setters and getters
    private string getCurrentStage()
    {
        return PlayerPrefs.GetString("currentstage");
    }
    private void setCurrentStage(string s)
    {
        PlayerPrefs.SetString("currentstage",s);
    }

}

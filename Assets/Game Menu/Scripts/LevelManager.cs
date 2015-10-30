using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour {

    //keep the current level
    private int currentLevel;

    //Button Next/Prev level
    public Button buttonNextLevel;
    public Button buttonPrevLevel;

    //Level Images
    public Sprite imageLevel1;
    public Sprite imageLevel2;
    public Sprite imageLevel3;
    public Sprite imageLevel4;
    public Sprite imageLevel5;
    public Sprite imageLevel6;
    public Sprite imageLevel7;
    public Sprite imageLevel8;
    public Sprite imageLevel9;
    public Sprite imageLevel10;
    //Where to insert them
    public Image imageLevel;


    //These are all the dynamic window Text objects 
    public Text textScore;
    public Text textLevelTitle;
    public Text textLevelCounter;
    public Text textLevelDescription;
    public Text textLevelScore;




    //This is for the Panel with the Lock
    public GameObject panelLocker;
   
   
    void Start()
    {
        //Initialize the window based on PlayerPrefs

        //find the currentLevel
        currentLevel = PlayerPrefs.GetInt("currentlevel");
        //fix text & other for level
        setProperLevel();
        //Handle Next/Prev buttons
        handleButtons();
       
        
    }

    void Update()
    {
        //displayScore
        textScore.text = "SCORE : " + PlayerPrefs.GetInt("score");
    }
    //This function will prepare the game to display the current level
    //and the navigation between Levels and Stages
    private void initializeWindow()
    {

    }

    void StoreHighScore(int newHighscore)
    {
        int oldHighscore = PlayerPrefs.GetInt("highscore", 0);
        if (newHighscore > oldHighscore)
        {
            PlayerPrefs.SetInt("hishscore",newHighscore);
        }
    }

    //Button listeners for the Level
   public void gotoNextLevel()
    {

       //increase the current level
       currentLevel++;
       PlayerPrefs.SetInt("currentlevel", currentLevel);
       setProperLevel();
       handleButtons();

       //when level changes set the quit stage
       PlayerPrefs.SetString("stagebuttonpressed", "quiz");

    }
    public void gotoPreviousLevel()
    {
        //decrease the current level
        currentLevel--;
        PlayerPrefs.SetInt("currentlevel", currentLevel);
        setProperLevel();
        handleButtons();
        //when level changes set the quit stage
        PlayerPrefs.SetString("stagebuttonpressed", "quiz");
    }
    private void setProperLevel()
    {
        

        switch (currentLevel)
        {
            case 1:
                imageLevel.sprite = imageLevel1; //fix the image
                textLevelCounter.text = "1 / 10";//the counter below the image
                textLevelTitle.text = "LEARN THE BASICS";//set the Title above the image
                textLevelDescription.text = PlayerPrefs.GetString("leveldescription1");//set the description
                textLevelScore.text = "LEVEL SCORE : "+ Convert.ToString(PlayerPrefs.GetInt("levelscore1"));
                panelLocker.SetActive(false);
                break;
            case 2:
                imageLevel.sprite = imageLevel2;
                textLevelCounter.text = "2 / 10";
                textLevelTitle.text = "Level2";//set the Title above the image
                textLevelDescription.text = PlayerPrefs.GetString("leveldescription2");//set the description
                textLevelScore.text = "LEVEL SCORE : "+ Convert.ToString(PlayerPrefs.GetInt("levelscore2"));
                panelLocker.SetActive(true);
                break;
            case 3:
                imageLevel.sprite = imageLevel3;
                textLevelCounter.text = "3 / 10";
                textLevelTitle.text = "Level3";//set the Title above the image
                break;
            case 4:
                imageLevel.sprite = imageLevel4;
                textLevelCounter.text = "4 / 10";
                textLevelTitle.text = "Level4";//set the Title above the image
                break;
            case 5:
                imageLevel.sprite = imageLevel5;
                textLevelCounter.text = "5 / 10";
                textLevelTitle.text = "Level5";//set the Title above the image
                break;
            case 6:
                imageLevel.sprite = imageLevel6;
                textLevelCounter.text = "6 / 10";
                textLevelTitle.text = "Level6";//set the Title above the image
                break;
            case 7:
                imageLevel.sprite = imageLevel7;
                textLevelCounter.text = "7 / 10";
                textLevelTitle.text = "Level7";//set the Title above the image
                break;
            case 8:
                imageLevel.sprite = imageLevel8;
                textLevelCounter.text = "8 / 10";
                textLevelTitle.text = "Level8";//set the Title above the image
                break;
            case 9:
                imageLevel.sprite = imageLevel9;
                textLevelCounter.text = "9 / 10";
                textLevelTitle.text = "Level9";//set the Title above the image
                break;
            case 10:
                imageLevel.sprite = imageLevel10;
                textLevelCounter.text = "10 / 10";
                textLevelTitle.text = "Level10";//set the Title above the image
                break;
            

        }
    }
    private void handleButtons()
    {
        if (currentLevel == 1)
        {
            buttonPrevLevel.interactable = false;
        }
        else if (currentLevel == 10)
        {
            buttonNextLevel.interactable = false;
        }
        else
        {
            buttonNextLevel.interactable = true;
            buttonPrevLevel.interactable = true;
        }
    }
}

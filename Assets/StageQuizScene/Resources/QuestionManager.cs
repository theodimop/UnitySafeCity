using UnityEngine;
using UnityEngine.UI;
using System.Xml.Linq;
using System.Linq;

public class QuestionManager : MonoBehaviour
{
    private Button buttonAnswer1, buttonAnswer2, buttonAnswer3;
    private Text ButtonText1, ButtonText2, ButtonText3;
    private Text questionText, resultText, timeText, scoreText;
    private string correctAnswer;
    private Image img;
    private Sprite spr;
    private int id, score;
    private float timer;
    

    void Start()
    {
        Initialize();
        DisplayQuestion();
    }

    public void Initialize()
    {
        /* Initialize question id and score. */
        id = 0;
        score = 0;

        /* Get connection with all elements in Scene in order to edit them and write 
           question information to them. */
        questionText = GameObject.Find("QuestionText").GetComponent<Text>();
        img = GameObject.Find("QuizImage").GetComponent<Image>();
        buttonAnswer1 = GameObject.Find("ButtonAnswer1").GetComponent<Button>();
        buttonAnswer2 = GameObject.Find("ButtonAnswer2").GetComponent<Button>();
        buttonAnswer3 = GameObject.Find("ButtonAnswer3").GetComponent<Button>();
        ButtonText1 = GameObject.Find("ButtonText1").GetComponent<Text>();
        ButtonText2 = GameObject.Find("ButtonText2").GetComponent<Text>();
        ButtonText3 = GameObject.Find("ButtonText3").GetComponent<Text>();
        resultText = GameObject.Find("ResultText").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        timeText = GameObject.Find("Time").GetComponent<Text>();

    }

    public void DisplayQuestion()
    {
        /* Set timer price each time to 20sec. (There is a 1 second decrease when calling
           Timer function.) */
        timer = 21f;
        
        /* Setting up default settings in each question after the first one. */
        if(id>=1)
        {
            EnableButtons();
        }

        /* Increment id and show his current score. */
        id++;
        scoreText.text = "Score: " + score;
        
        /* Loading data from an XML file. */
        var xml = XDocument.Load("Assets/StageQuizScene/Resources/Questions.xml");


        /* Getting the question data using linq in order to extract the information from
           the xml tags. */
        var questions = (from c in xml.Root.Descendants("question")
                        where c.Attribute("id").Value == "" + id
                        select new 
                        {
                            QuestionText = (string)c.Element("QuestionText").Value,
                            AnswerA = (string)c.Element("AnswerA").Value,
                            AnswerB = (string)c.Element("AnswerB").Value,
                            AnswerC = (string)c.Element("AnswerC").Value,
                            img = (string)c.Element("img").Value,
                            CorrectAnswer = (string)c.Element("CorrectAnswer").Value
                        });

        /* And showing them in the appropriate element in the scene panel. */
        foreach (var question in questions)
        {    
             questionText.text = question.QuestionText;
        
             if (!question.img.Equals("0"))
             {
                 img.enabled = true;
                 img.sprite = Resources.Load<Sprite>(question.img);
             }
             else
             {
                 img.enabled = false;
             }

             ButtonText1.text = question.AnswerA;
             ButtonText2.text = question.AnswerB;
             

            if (!question.AnswerC.Equals("0"))
            {
                ButtonText3.text = question.AnswerC;
            }
            else
            {
                buttonAnswer3.gameObject.SetActive(false);
            }

            correctAnswer = question.CorrectAnswer;
       }
       
       /* Start the timer. */
       Timer();

    }

    /* This function is responsible for the countdown timer. */
    public void Timer ()
    {
        /* Decrease the timer and then show the leftover time in the panel. */
        timer -= 1f;
        timeText.text = "Time: " + timer.ToString();

        /* If the 20sec have ended disable buttons and call CorrectAnswerSelected with the
           appropriate arguments. */
        if (timer<=0)
        {
            DisableButtons();
            CorrectAnswerSelected("end of time", "");
        }
        else
        {
            /* Else add listeners to all the buttons and if a button is clicked call 
               DisableButtons, stopTimer and CorrectAnswerSelected with the appropriate
               arguments. */
            buttonAnswer1.onClick.AddListener(() =>
            {
                DisableButtons();
                stopTimer(timer);
                CorrectAnswerSelected(ButtonText1.text, correctAnswer);
                
            });

            buttonAnswer2.onClick.AddListener(() =>
            {
                DisableButtons();
                stopTimer(timer);
                CorrectAnswerSelected(ButtonText2.text, correctAnswer);
                
            });

            buttonAnswer3.onClick.AddListener(() =>
            {
                DisableButtons();
                stopTimer(timer);
                CorrectAnswerSelected(ButtonText3.text, correctAnswer);

            });

            /* If no button is pressed and time has not exceeded call recursively the Timer
               function (after waiting for 1sec). */
            Invoke("Timer", 1f);
        }
        
    }
    
    /* If the user has answered stop the timer cancelling the invoke from timer function
       and show the stopped timer's price on scene. */
    public void stopTimer(float time)
    {
        CancelInvoke();
        timeText.text = "Time: " + time.ToString();   
    }

    /* Restoring default settings each time id is increased by enabling disabled buttons. 
       Also update his current score. */
    public void EnableButtons()
    {
        buttonAnswer1.enabled = true;
        buttonAnswer2.enabled = true;
        buttonAnswer3.enabled = true;
        buttonAnswer3.gameObject.SetActive(true);
        ScoreIncrement(resultText.text.Equals("Σωστή απάντηση!"));
        resultText.gameObject.SetActive(false);
    }

    /* Disabling buttons (buttons not able to be clicked) each time the user has already 
       answered the question. */
    public void DisableButtons()
    {
        buttonAnswer1.enabled = false;
        buttonAnswer2.enabled = false;
        buttonAnswer3.enabled = false;
    }

    /* Using this function to see if user selected the correct answer. */
    public void CorrectAnswerSelected(string selectedAnswerText, string correctAnswer)
    {
        /* If time has not exceeded */
        if (selectedAnswerText != "end of time")
        {
            bool result = string.Equals(selectedAnswerText, correctAnswer);

            /* if the user has pressed the correct answer */
            if (result == true)
            {
                resultText.color = Color.green;
                resultText.text = "Σωστή απάντηση!";   
            }
            /* or not */
            else
            {
                resultText.color = Color.red;
                resultText.text = "Λάθος απάντηση";
            }
        }
        /* else if there is an exceed in time */
        else
        {
            resultText.color = Color.black;
            resultText.text = "Τέλος χρόνου.";
        }
        /* show the results*/
        resultText.gameObject.SetActive(true);
        /* call function DisplayQuestion to display the new question in 2 seconds.*/
        Invoke("DisplayQuestion", 2f);
        
    }
    
    /* If the user has answered right increment his score. Else leave it as it is. */
    public int ScoreIncrement(bool result)
    {
        if (result)
        {
            return score = score + 50;
        }
        return score;
    }
}
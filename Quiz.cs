//Quiz Script
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<Questions> questions = new List<Questions>();
    Questions currentQuestion;
    public int answeredQuestions = 0;
    public const int questionsCount = 15;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite AnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
 
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    [Header("Audio")]
    AudioManager audioManager;

    public bool isComplete;    

    void Awake()
    { 
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioManager = FindObjectOfType<AudioManager>();
        progressBar.maxValue = questionsCount;
        progressBar.value = 0; 
    }

   void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        if(progressBar.value == progressBar.maxValue)
        {
            isComplete = false; //change the value with "true"
            return;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
      if(progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
            return;
        }

    }


   public void DisplayAnswer(int index)
    {
       Image buttonImage;
       Image correctAnswerImage;

        if(index == currentQuestion.GetCorrectAnswer())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            answerButtons[index].GetComponent<Image>().color = new Color(0.3720272f, 0.7735849f, 0.09779279f, 1.0f); //Green
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
            audioManager.PlayCorrectAnswerClip();
        }
      
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswer ();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

            //handling for correct answer 
            correctAnswerImage = answerButtons[currentQuestion.GetCorrectAnswer()].GetComponent<Image>();
            answerButtons[currentQuestion.GetCorrectAnswer()].GetComponent<Image>().color = new Color(0.3720272f, 0.7735849f, 0.09779279f, 1.0f); //Green


            if (index >= 0)
             {
                answerButtons[index].GetComponent<Image>().color = new Color(0.8018868f, 0.04085077f, 0.1125426f, 1.0f); //Red
                buttonImage.sprite = correctAnswerSprite;
                audioManager.PlayWrongAnswerClip();
             }
             
        }
        SetButtonState(false);
        timer.CancelTimer();
    }
       
    void GetNextQuestion()
    {
        if(answeredQuestions <= 15)
        {
           SetButtonState(true);
           SetDefaultButtonSprites();
           GetRandomQuestion();
           DisplayQuestion();
           progressBar.value++;
           answeredQuestions++;
           scoreKeeper.IncrementQuestionsSeen();
        }
       
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion))
        {
           questions.Remove(currentQuestion);  
        }
       
    }

    void DisplayQuestion()
    {
     questionText.text = currentQuestion.GetQuestion(); 

       for(int i = 0; i <answerButtons.Length; i++)
       {
           TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }

    }

    void SetDefaultButtonSprites()
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            answerButtons[i].GetComponent<Image>();
            answerButtons[i].GetComponent<Image>().color = new Color(0.1333333f, 0.5803922f, 0.8588236f, 1.0f);
            buttonImage.sprite = AnswerSprite;
        }

    }

}

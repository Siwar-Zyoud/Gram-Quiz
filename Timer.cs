// Timer Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Timer : MonoBehaviour
{
   [SerializeField] float timeToCompleteQuestion = 30f;
   [SerializeField] float timeToShowCorrectAnswer = 3f;

   public bool loadNextQuestion;
   public bool isAnsweringQuestion = false;
   public float fillFraction;
   bool isQuestionTimerStarted = true;
   bool endTimerAudioPlayed; 

   float timerValue;
   AudioManager audioManager;

    void Awake() 
    {
      audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
      UpdateTimer();   
    }

    public void CancelTimer()
    {
        
      timerValue = 0;
        
    }
    
    void UpdateTimer()
    {
       
        timerValue -= Time.deltaTime;

        if(isAnsweringQuestion)
        {
          if(timerValue > 0)
          {
            fillFraction = timerValue / timeToCompleteQuestion;

            if (fillFraction <= 0.1f && !endTimerAudioPlayed)
            {
              audioManager.PlayEndTimerClip();
              endTimerAudioPlayed = true;          
            }

            if(isQuestionTimerStarted)
            { 
              endTimerAudioPlayed = false;    
              audioManager.PlayBeginTimerClip();
              isQuestionTimerStarted = false;
            } 
          }
          else
          {
            isAnsweringQuestion = false;
            timerValue = timeToShowCorrectAnswer; 
            isQuestionTimerStarted = true;
          } 
        }

        else
        {
            if(timerValue > 0)
            {
              fillFraction = timerValue / timeToShowCorrectAnswer; 
            }
             else
            {
              isAnsweringQuestion = true;
              timerValue = timeToCompleteQuestion;
              loadNextQuestion = true; 
            }  

        }
    
    }

}

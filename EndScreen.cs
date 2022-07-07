// End Screen Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        if(scoreKeeper.CalculateScore() < 50)
        {
            finalScoreText.text = "Better Luck Next Time!\nYou got a score of " + 
                                scoreKeeper.CalculateScore() + "%";
        }
        if(scoreKeeper.CalculateScore() >= 50)
        {
         finalScoreText.text = "Congratulations!\nYou got a score of " + 
                                scoreKeeper.CalculateScore() + "%";   
        }
        
    }
}

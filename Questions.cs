//Questions Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Questions", order = 1)]
public class Questions : ScriptableObject 
{
   [TextArea(4,8)]
   [SerializeField] string question = "Enter new question text here"; 
   [SerializeField] string[] answers = new string[3];
   [SerializeField] int correctAnswerIndex;

   public string GetQuestion()
   {
    return question;
   }

   public string GetAnswer(int index)
   {
    return answers[index];
   }

   public int GetCorrectAnswer()
   {
    return correctAnswerIndex;
   }
}






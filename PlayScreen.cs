// Play Screen Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScreen : MonoBehaviour
{
   public void Welcome()
   {
    SceneManager.LoadScene("GramQuizScene");
   } 
}

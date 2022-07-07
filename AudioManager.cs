// Audio Manager Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Correct Answer")]
    [SerializeField] AudioClip correctAnswerClip;
    [SerializeField] [Range(0f, 1f)] float correctAnsVolume = 1f;

    [Header("Wrong Answer")]
    [SerializeField] AudioClip wrongAnswerClip;
    [SerializeField] [Range(0f, 1f)] float wrongAnsVolume = 1f;

    [Header("Begin Timer")]
    [SerializeField] AudioClip beginTimerClip;
    [SerializeField] [Range(0f, 1f)] float beginTimerVolume = 0.3f;

    [Header("End Timer")]
    [SerializeField] AudioClip endTimerClip;
    [SerializeField] [Range(0f, 1f)] float endTimerVolume = 1f;

    public void PlayCorrectAnswerClip()
    {
        PlayClip(correctAnswerClip, correctAnsVolume);  
    }

    public void PlayWrongAnswerClip()
    {
        PlayClip(wrongAnswerClip, wrongAnsVolume);
    }

    public void PlayBeginTimerClip()
    {
        PlayClip(beginTimerClip, beginTimerVolume);  
    }

    public void PlayEndTimerClip()
    {
        PlayClip(endTimerClip, endTimerVolume);
        
    }
    
    private void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    } 
}

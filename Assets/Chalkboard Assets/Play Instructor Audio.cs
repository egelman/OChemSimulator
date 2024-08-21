using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayInstructorAudio : MonoBehaviour
{
    public AudioSource audioSource; 
    public void OnButtonClick()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}


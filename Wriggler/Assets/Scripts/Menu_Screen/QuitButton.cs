using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public AudioSource buttoclick;

    public void PlayButtonSound()
    {
        buttoclick.Play();
    }

    public void QuitGame () 
    {
        Application.Quit ();
        Debug.Log("Game is exiting");
        //Just to make sure its working
    }
    
    
}

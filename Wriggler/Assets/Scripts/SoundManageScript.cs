using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManageScript : MonoBehaviour
{
    public AudioSource walkingSound;
    public AudioSource jumpingSound;

    public void PlayJumpSound()
    {
        jumpingSound.Play();
    }
    public void PlayJumpingSound()
    {
        if (!jumpingSound.isPlaying)
        {
            jumpingSound.Play();
        }
    }

    public void PlayWalkingSound()
    {
        walkingSound.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManageScript : MonoBehaviour
{
    public AudioSource walkingSound;
    public AudioSource jumpingSound;
    public AudioSource hurtSound;
    public AudioSource deadSound;
    public AudioSource wallslideSound;
    public AudioSource fallSound;
    public AudioSource pauseSound;

    public void PlayPauseSound()
    {
        pauseSound.Play();
    }

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

    public void PlayDeadSound()
    {
        deadSound.Play();
    }

    public void PlayHurtSound()
    {
        hurtSound.Play();
    }

    public void PlayWallSlideSound()
    {
        wallslideSound.Play();
    }

    public void FallSound()
    {
        fallSound.Play();
    }
}

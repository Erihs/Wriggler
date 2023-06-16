using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseWithAnim : MonoBehaviour
{
    public void FreezeGame()
    {
        Time.timeScale = 0f;
    }
    
    public SoundManageScript soundManager;
    public void PausedSound()
    {
        soundManager.PlayPauseSound();
    }
}

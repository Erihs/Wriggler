using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public int gameStartScene;

    private void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
}

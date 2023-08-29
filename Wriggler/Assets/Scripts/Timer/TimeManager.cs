using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeText;
    private float totalTime;

    private static TimeManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        totalTime += Time.deltaTime;
        UpdateUIText();
        Debug.Log("F1");
    }

    private void UpdateUIText()
    {
        timeText.text = "Total Time: " + totalTime.ToString("F1");
    }
}
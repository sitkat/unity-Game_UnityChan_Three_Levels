using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image DeathScreen;
    Image timerBar;
    public float TimeSec;
    float timeLeft;
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = TimeSec;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft;
        }
        else
        {
            DeathScreen.gameObject.SetActive(true);
        }
    }
}

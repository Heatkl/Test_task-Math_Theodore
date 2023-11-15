using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Class for control gameplay time
public class GameTimer : MonoBehaviour
{
    public static float timeRemaining;                      //static variable of current time

    public delegate void FinishEventHandler();
    public static event FinishEventHandler TimerFinished;   //Event for time end action

    public TMP_Text timerText;                              //Display timer text

    Image timerImage;                                       //Image of time visualization

    float maxTime = 60;                                     //Max time of game
    bool isFinished = true;                                 //Bool var for control timer state



    private void Start()
    {
        timerImage = GetComponent<Image>();
    }
    private void Update()
    {
        CheckTimer();
    }

    //Method for add bonus time
    public void AddTimerValue(int bonus)
    {
        timeRemaining += bonus;
    }
    //Method for set timer state
    void CheckTimer()
    {
        if (isFinished)
            return;
        //Check timer max values
        if (timeRemaining > maxTime)
            timeRemaining = maxTime;

        //Check timer
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isFinished = true;
            TimerFinished();
        }
        else
        {
            timerText.text = (int)timeRemaining == 60 ? "00" + ((int)timeRemaining / 60) + ":00" : (int)timeRemaining >= 10 ? "00:" + (int)timeRemaining : "00:0" + (int)timeRemaining;
            timerImage.fillAmount = timeRemaining / maxTime;
            timeRemaining -= Time.deltaTime;
        }
    }

    public void RestartTimer()
    {
        timeRemaining = maxTime;
        isFinished = false;
    }

    
}

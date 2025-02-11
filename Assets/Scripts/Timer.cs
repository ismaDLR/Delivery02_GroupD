using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float seconds;
    private float minutes;
    private String text;

    private const int timeToChangeTextSeconds = 10;
    private const int timeToChangeTextMinutes = 60;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seconds = 55;
        minutes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;

        if ((int) minutes < timeToChangeTextSeconds)
        {
            if ((int) seconds < timeToChangeTextSeconds)
            {
                text = "0" + minutes + ":0" + (int) seconds;
            }
            else
            {
                text = "0" + minutes + ":" + (int) seconds;
            }
        }
        else
        {
            if ((int) seconds < timeToChangeTextSeconds)
            {
                text = minutes + ":0" + (int) seconds;
            }
            else
            {
                text = minutes + ":" + (int) seconds;
            }
        }

        if ((int) seconds == timeToChangeTextMinutes)
        {
            seconds = 0;
            minutes++;
        }

        gameObject.GetComponent<Text>().text = text;
    }
}

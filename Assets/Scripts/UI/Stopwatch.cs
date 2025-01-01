using TMPro;
using UnityEngine;
using System;

public class Stopwatch : MonoBehaviour
{
    public static bool isGoing = false;
    private static float currentTime;
    public Transform textTransform;
    private TMP_Text text;

    private void Start()
    {
        currentTime = 0;
        text = textTransform.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (isGoing)
        {
            currentTime += Time.deltaTime;
            text.text = "";
            TimeSpan time = TimeSpan.FromSeconds(currentTime);

            if (time.Minutes < 10)
            {
                text.text += "0";
            }

            text.text += time.Minutes.ToString();
            text.text += ":";

            if (time.Seconds < 10)
            {
                text.text += "0";
            }
            text.text += time.Seconds.ToString();
            text.text += ":";

            if (time.Milliseconds < 10)
            {
                text.text += "00";
            }
            else if (time.Milliseconds < 100)
            {
                text.text += "0";
            }
            text.text += time.Milliseconds.ToString();
        }
    }

    public static void reset()
    {
        currentTime = 0f;
    }

    public static void startStopwatch()
    {
        isGoing = true;
    }

    public static void stopStopwatch()
    {
        isGoing = false;
    }
}

using TMPro;
using UnityEngine;
using System;

public class Stopwatch : MonoBehaviour
{
    public static bool isGoing = false;
    private static float currentTime;
    public static TimeSpan bestTime;
    public static TimeSpan finalTime;
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
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            text.text = time.ToString(@"mm\:ss\:fff");
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

    public static void updateBestTime()
    {
        finalTime = TimeSpan.FromSeconds(currentTime);
        if (TimeSpan.Compare(finalTime, bestTime) == -1)
        {
            bestTime = finalTime;
        } else if (bestTime == TimeSpan.Zero)
        {
            bestTime = finalTime;
        }
    }
}

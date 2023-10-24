using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime = 0f;
    private bool timerActive = false;

    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public void ResetTimer()
    {
        currentTime = 0f;
    }
}

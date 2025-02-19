using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public RectTransform TimeMaxLine;
    public RectTransform TimeLine;
    public Text TimeText;
    private float maxTime;
    private float currentTime;
    
    private bool isCounting;
    private Action TimeZeroEvent;

    private float usedTime;
    
    private void Awake()
    {
        SetTime();
    }

    public void SetTimeCounter(float maxTime, Action TimeZeroEvent)
    {
        isCounting = false;
        this.maxTime = maxTime;
        this.currentTime = maxTime;
        this.TimeZeroEvent = TimeZeroEvent;
        usedTime = 0;
    }

    public void StartCount()
    {
        isCounting = true;
    }

    public void StopCount()
    {
        isCounting = false;
    }

    public float GetUsedTime()
    {
        return usedTime;
    }

    public void RewardTime(float rewardRate)
    {
        currentTime = Mathf.Min(currentTime + maxTime * rewardRate, maxTime);
    }
    
    private void Update()
    {
        if (isCounting)
        {
            usedTime += Time.deltaTime;
            currentTime -= Time.deltaTime;
            SetTime();
            if (currentTime <= 0)
            {
                StopCount();
                TimeZeroEvent?.Invoke();
                TimeZeroEvent = null;
            }
        }
    }

    private void SetTime()
    {
        float rate = maxTime != 0 ? Mathf.Clamp(currentTime / maxTime, 0, 1) : 1;
        float currentWidth = TimeMaxLine.sizeDelta.x * rate;
        TimeLine.sizeDelta = new Vector2(currentWidth, TimeLine.sizeDelta.y);
        TimeText.text = (currentTime / 60).ToString("00") + ":" + (currentTime % 60).ToString("00");
    }
}

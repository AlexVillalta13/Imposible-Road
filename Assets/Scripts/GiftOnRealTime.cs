﻿using Bayat.SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftOnRealTime : MonoBehaviour
{
    string timeSaved = "savedTime";
    DateTime lastTimePlayed;
    TimeSpan timeToShow = TimeSpan.Zero;
    [SerializeField] int hoursToGetGift = 12;
    TimeSpan hoursToGiftTimeSpan;

    public event Action<bool, TimeSpan> canPlayerGetGift;

    private void Awake()
    {
        hoursToGiftTimeSpan = new TimeSpan(hoursToGetGift, 0, 0);
    }

    async void Start()
    {
        if (await SaveSystemAPI.ExistsAsync(timeSaved) == false)
        {
            return;
        }
        lastTimePlayed = await SaveSystemAPI.LoadAsync<DateTime>(timeSaved);
    }

    void Update()
    {
        CalculateTimeDifference();
        CanGetGift();
    }

    public void StartTimer()
    {
        lastTimePlayed = DateTime.Now;
        SaveSystemAPI.SaveAsync(timeSaved, lastTimePlayed);
    }

    private void CalculateTimeDifference()
    {
        if (lastTimePlayed == null) { return; }

        TimeSpan timeDifference = DateTime.Now - lastTimePlayed;
        timeToShow = hoursToGiftTimeSpan - timeDifference;
    }

    private void CanGetGift()
    {
        if(timeToShow > TimeSpan.Zero)
        {
            canPlayerGetGift?.Invoke(false, timeToShow);
        }
        else
        {
            canPlayerGetGift?.Invoke(true, TimeSpan.Zero);
        }
    }
}

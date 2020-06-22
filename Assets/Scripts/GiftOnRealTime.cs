using Bayat.SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftOnRealTime : MonoBehaviour
{
    string timeSaved = "savedTime";
    DateTime lastTimePlayed;
    TimeSpan timeToShow;
    [SerializeField] int hoursToGetGift = 12;
    TimeSpan hoursToGiftTimeSpan;

    public event Action<bool, TimeSpan> canPlayerGetGift;

    private void Awake()
    {
        // TODO change to hours
        hoursToGiftTimeSpan = new TimeSpan(0, 0, hoursToGetGift);
    }

    async void Start()
    {
        if (await SaveSystemAPI.ExistsAsync(timeSaved) == false)
        {
            StartTimer();
        }
        lastTimePlayed = await SaveSystemAPI.LoadAsync<DateTime>(timeSaved);
    }

    void Update()
    {
        Debug.Log("Current Time: " + System.DateTime.Now);

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
        TimeSpan timeDifference = DateTime.Now - lastTimePlayed;
        timeToShow = hoursToGiftTimeSpan - timeDifference;
        Debug.Log("Time to show: " + timeToShow);

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

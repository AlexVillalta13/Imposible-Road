using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyGiftDisplay : MonoBehaviour
{
    TextMeshProUGUI countdownText;
    Button giftButton;

    GiftOnRealTime giftOnRealTime;

    private void Awake()
    {
        countdownText = GetComponentInChildren<TextMeshProUGUI>();
        giftButton = GetComponentInChildren<Button>();

        giftOnRealTime = FindObjectOfType<GiftOnRealTime>();
    }

    private void OnEnable()
    {
        giftOnRealTime.canPlayerGetGift += SetupButtonAndText;
    }

    private void OnDisable()
    {
        giftOnRealTime.canPlayerGetGift -= SetupButtonAndText;
    }

    private void SetupButtonAndText(bool canGetGift, TimeSpan countdown)
    {
        int hours = countdown.Hours;
        int minutes = countdown.Minutes;
        int seconds = countdown.Seconds;
        countdownText.text = String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        if (canGetGift)
        {
            giftButton.interactable = true;
        }
        else
        {
            giftButton.interactable = false;
        }
    }
}

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
    ButtonHighlightAnimation buttonHighlightAnimation;

    private void Awake()
    {
        countdownText = GetComponentInChildren<TextMeshProUGUI>();
        giftButton = GetComponentInChildren<Button>();

        giftOnRealTime = FindObjectOfType<GiftOnRealTime>();
        buttonHighlightAnimation = GetComponentInChildren<ButtonHighlightAnimation>();
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
        if(countdown == TimeSpan.Zero)
        {
            countdownText.text = "FREE GIFT";
        } 
        else
        {
            int hours = countdown.Hours;
            int minutes = countdown.Minutes;
            int seconds = countdown.Seconds;
            countdownText.text = String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        if (canGetGift)
        {
            giftButton.interactable = true;
            buttonHighlightAnimation.StartHighlightAnimation();
        }
        else
        {
            giftButton.interactable = false;
        }
    }
}

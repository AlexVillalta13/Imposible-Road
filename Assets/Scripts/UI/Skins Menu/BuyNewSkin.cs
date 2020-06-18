using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyNewSkin : MonoBehaviour
{
    [SerializeField] int skinCost = 100;
    [SerializeField] Transform openBoxScreen = null;

    UIHiderOnGameLoop UIHiderOnGameLoop;
    GemsManager gemsManager;


    private void Awake()
    {
        UIHiderOnGameLoop = GetComponentInParent<UIHiderOnGameLoop>();
        gemsManager = FindObjectOfType<GemsManager>();
    }

    public void BuySkinButtonPressed()
    {
        bool canPay = gemsManager.PaySomething(skinCost);

        if(canPay)
        {
            ShowOpenBoxScreen();
        }
        else
        {

        }
    }

    private void ShowOpenBoxScreen()
    {
        UIHiderOnGameLoop.EnableUI(openBoxScreen);
    }
}

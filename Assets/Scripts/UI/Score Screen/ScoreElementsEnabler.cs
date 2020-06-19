using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreElementsEnabler : MonoBehaviour
{
    [Range(0f, 100f)]
    [SerializeField] float probabilityToShowAd = 50f;

    [SerializeField] GameObject seeAdPanel = null;
    [SerializeField] GameObject buySkinPanel = null;

    GemsManager gemsManager;
    SkinSystem skinSystem;

    private void Awake()
    {
        gemsManager = FindObjectOfType<GemsManager>();
        skinSystem = FindObjectOfType<SkinSystem>();
    }

    private void OnEnable()
    {
        CheckToEnableBuySkinPanel();
        CheckToEnableSeeAdPanel();
    }

    private void CheckToEnableBuySkinPanel()
    {
        if (skinSystem.GetNotOwnedSkinCount() <= 0 || gemsManager.GetGemsCount() < 100)
        {
            EnableBuySkinPanel(false);
        }
        else
        {
            EnableBuySkinPanel(true);
        }
    }

    private void EnableBuySkinPanel(bool state)
    {
        buySkinPanel.SetActive(state);
    }

    private void CheckToEnableSeeAdPanel()
    {
        float randomNumber = Random.Range(0f, 100f);
        if(randomNumber <= probabilityToShowAd)
        {
            EnableSeeAdPanel(true);
        }
        else
        {
            EnableSeeAdPanel(false);
        }
    }

    private void EnableSeeAdPanel(bool state)
    {
        seeAdPanel.SetActive(state);
    }
}

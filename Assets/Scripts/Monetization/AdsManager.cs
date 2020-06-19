using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    string gameID = "3665058";
#endif
#if UNITY_ANDROID
    string gameID = "3665059";
#endif
    string rewardedVideo = "rewardedVideo";

    GemsManager gemsManager;
    [SerializeField] int gemsEarnedWithAd = 50;

    private void Awake()
    {
        gemsManager = FindObjectOfType<GemsManager>();
    }

    private void Start()
    {
        Advertisement.Initialize(gameID);
    }

    public void ShowRewardedVideoAd()
    {
        Advertisement.AddListener(this);
        Advertisement.Show(rewardedVideo);
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogWarning(message);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            gemsManager.AddGems(gemsEarnedWithAd);
        }
        else if(showResult == ShowResult.Skipped)
        {
            // Do nothing
        }
        else if(showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }
}

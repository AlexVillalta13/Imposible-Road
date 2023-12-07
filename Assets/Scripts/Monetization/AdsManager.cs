using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
#if UNITY_IOS
    string gameID = "3665058";
    string rewardedVideo = "iOS_Rewarded";
#endif
#if UNITY_ANDROID
    string gameID = "3665059";
    string rewardedVideo = "Android_Rewarded";
#endif


    [SerializeField] bool testMode = false;
    [SerializeField] EventSystem eventSystemPrefab;

    GemsManager gemsManager;
    [SerializeField] int gemsEarnedWithAd = 50;

    private void Awake()
    {
        gemsManager = FindObjectOfType<GemsManager>();

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, testMode, this);
        }
    }
    public void OnInitializationComplete()
    {
        //var es = FindObjectsOfType<EventSystem>();
        //foreach (var e in es)
        //{
        //    Destroy(e.gameObject);
        //}
        //Instantiate(eventSystemPrefab);

        Advertisement.Load(rewardedVideo, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }

    public void ShowRewardedVideoAd()
    {
        Advertisement.Show(rewardedVideo, this);
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

    public void OnUnityAdsDidStart(string placementId) { }

    public void OnUnityAdsReady(string placementId) { }

    public void OnUnityAdsAdLoaded(string placementId) { }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogWarning("The ad did not finish due to an error. Message: " + message);
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        gemsManager.AddGems(gemsEarnedWithAd);
        Advertisement.Load(rewardedVideo, this);
    }
}

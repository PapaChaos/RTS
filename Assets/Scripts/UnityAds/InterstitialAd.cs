using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
#if UNITY_IOS

    string _adUnitId = "Interstitial_iOS";
	//bool mobilePlatform = true;

#elif UNITY_ANDROID
    string _adUnitId = "Interstitial_Android";
    //bool mobilePlatform = true;

#else
    string _adUnitId;
	//bool mobilePlatform = false;
#endif

    [SerializeField]
    MainMenu mm;

    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        ShowAd();
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }


    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) 
    {
    }
    public void OnUnityAdsShowClick(string adUnitId) 
    {
        Debug.LogWarning("Unity ad clicked");
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) 
    {

        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
		{
            Debug.LogWarning("Unity ad completed");
            mm.trainingScene();
		}
        if(showCompletionState == UnityAdsShowCompletionState.SKIPPED)
		{
            Debug.LogWarning("Unity ad skipped");
            mm.trainingScene();
        }
        if(showCompletionState == UnityAdsShowCompletionState.UNKNOWN)
		{
            Debug.LogWarning("Unity ad Unknown");
            //mm.trainingScene();
        }
    }
}

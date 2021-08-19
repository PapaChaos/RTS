using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
#if UNITY_IOS

    string _gameId = "4251008";
	bool mobilePlatform = true;

#elif UNITY_ANDROID
    string _gameId = "4251009";
    bool mobilePlatform = true;

#else
    string _gameId;
	bool mobilePlatform = false;
#endif

    bool _testMode = true;
    bool _enablePerPlacementMode = true;

    void Awake()
    {
        InitializeAds();
        /* FOR RELEASE.
        if (mobilePlatform)
        InitializeAds();
		else
		{
            Destroy(this);
		}*/
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(_gameId, _testMode, _enablePerPlacementMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}

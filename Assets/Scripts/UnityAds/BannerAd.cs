using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class BannerAd : MonoBehaviour
{
#if UNITY_IOS

    string _adUnitId = "Banner_iOS";
	//bool mobilePlatform = true;

#elif UNITY_ANDROID
    string _adUnitId = "Banner_Android";
    //bool mobilePlatform = true;

#else
    string _adUnitId;
	//bool mobilePlatform = false;
#endif

    void Start()
    {
        LoadBanner();
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Advertisement.Banner.Show(_adUnitId);
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        LoadBanner();
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
        Advertisement.Banner.Hide(true);
    }

    void OnDestroy()
    {
        //kill other stuff?
    }
}
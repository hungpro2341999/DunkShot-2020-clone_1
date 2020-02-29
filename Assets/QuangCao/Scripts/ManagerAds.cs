using System;
using UnityEngine;
using System.Collections;
#if USE_ADMOB
using GoogleMobileAds;
using GoogleMobileAds.Api;
#endif
#if USE_UNITY_ADS
using UnityEngine.Advertisements;
#endif
using UnityEngine.Events;

public enum BannerType
{
    Banner,
    SmartBanner
}
public class ManagerAds : MonoBehaviour
{
    public static ManagerAds Ins;
    [Header("Admob")]
    public BannerType bannerType = BannerType.SmartBanner;
    [Space(1)]
    public string ID_BANNER;
    public string ID_FULL;
    public string ID_REWARD;
    [Space(2)]
    [Header("Unity")]
    public string ID_UNITY_ADS;
    [Space(2)]
    [Header("Info App")]
    public string ID_MORE;

    private string AppID = "ca-app-pub-3940256099942544~3347511713";
    public bool isUseAdmob;
    public bool isUseUnityAds;
    public bool isUseFireBase;
    public bool isStartBanner;

    public string USE_ADMOB = "USE_ADMOB";
    public string USE_UNITY_ADS = "USE_UNITY_ADS";
    public string USE_FIREBASE = "USE_FIREBASE";
    private const string URL_RATE = "https://play.google.com/store/apps/details?id=";
    private const string URL_MORE = "https://play.google.com/store/apps/developer?id=";
    private bool triggerCompleteMethod = false;
    private const string REMOVE_ADS = "RemoveAds";
#if USE_ADMOB
    public BannerView bannerView;
    public InterstitialAd interstitial;
    public RewardBasedVideoAd rewardBasedVideo;
#endif

    private UnityAction<bool> OnCompleteMethod;
    private UnityAction OnInterstitialClosed;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
            DontDestroyOnLoad(this);
        }
        else if (Ins != this)
        {
            Destroy(gameObject);
        }
    }

    public void LogEvent(string name)
    {
#if USE_FIREBASE
        Firebase.Analytics.FirebaseAnalytics.LogEvent(name);
#endif
    }

    public void RemoveAds()
    {
        PlayerPrefs.SetInt(REMOVE_ADS, 1);
        HideBanner();
    }

    public bool CanShowAds()
    {
        return PlayerPrefs.GetInt(REMOVE_ADS, 0) == 0;
    }


    public void Start()
    {
#if USE_ADMOB
        MobileAds.Initialize("ca-app-pub-4162057951378139~4157250905");

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        this.rewardBasedVideo.OnAdLoaded += RewardBasedVideo_OnAdLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
        this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        RequestRewardBasedVideo();

        if (isStartBanner)
        {
            ShowBanner();
        }
        RequestInterstitial();

#endif
#if USE_UNITY_ADS
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(ID_UNITY_ADS);
        }
#endif


    }



    #region Share More Rate
    public void RateApp()
    {
        Application.OpenURL(URL_RATE + Application.identifier);
    }

    public void MoreGame()
    {
        Application.OpenURL(URL_MORE + ID_MORE);
    }
    public void ShareApp()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            //AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            //AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + imagePath);
            //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            //intentObject.Call<AndroidJavaObject>("setType", "image/png");

            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
                URL_RATE + Application.identifier);
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "");
            currentActivity.Call("startActivity", jChooser);
        }

    }
    #endregion

    #region Admob
#if USE_ADMOB
    public bool IsRewardVideoAvailable()
    {

        if (rewardBasedVideo != null)
        {
            if (!rewardBasedVideo.IsLoaded())
            {
                if (Advertisement.IsReady("video"))
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        return false;

    }
#endif
    // Returns an ad request with custom ad targeting.
    public void ShowInterstitial(UnityAction callback = null)
    {
        OnInterstitialClosed = callback;
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (callback != null)
            {
                callback.Invoke();
            }
            return;
        }
        if (!CanShowAds())
        {
            StartCoroutine(CompleteMethodInterstitial());
            return;
        }
#if USE_ADMOB
        if (!interstitial.IsLoaded())
        {
#if USE_UNITY_ADS
            if (Advertisement.IsReady("video"))
            {
                ShowFullUnity();
                return;
            }
#endif
            StartCoroutine(CompleteMethodInterstitial());
            return;
        }

        if (interstitial.IsLoaded())
        {

            interstitial.Show();
        }

#endif
    }

    public void ShowRewardedVideo(UnityAction<bool> callback)
    {
        triggerCompleteMethod = true;
        OnCompleteMethod = callback;
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (callback != null)
            {
                callback(true);
            }
            return;
        }

#if USE_ADMOB
        if (rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
        }
        else
        {
            if (IsRewardVideoAvailable())
                ShowRewardedVideoUnity();
        }
#endif
    }

    public void RequestRewardBasedVideo()
    {
#if USE_ADMOB
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, ID_REWARD);

#endif
    }


    public void ShowRewardedVideoUnity()
    {
#if USE_UNITY_ADS
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("rewardedVideo", options);
#endif
    }
    public void ShowFullUnity()
    {
#if USE_UNITY_ADS
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowFullResult;

        Advertisement.Show("video", options);
#endif
    }

    public void ShowBanner()
    {
        if (!CanShowAds())
        {
            return;
        }
#if USE_ADMOB
        bannerLoaded = false;
        if (bannerView != null)
        {
            bannerLoaded = true;
            bannerView.Show();
        }
        else
        {
            RequestBanner();
        }
#endif
    }
    private bool bannerLoaded;
    public void HideBanner()
    {
#if USE_ADMOB
        if (bannerView != null)
        {
            if (bannerLoaded == false)
            {
                bannerView.Destroy();
            }
            else
            {
                bannerView.Hide();
            }
        }
#endif
    }

#if USE_ADMOB
    public void RequestBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
        AdSize adSize = bannerType == BannerType.Banner ? AdSize.Banner : AdSize.SmartBanner;
        // Create a 320x50 banner at the top of the screen.

        this.bannerView = new BannerView(ID_BANNER, adSize, AdPosition.Bottom);
        bannerView.OnAdLoaded += BannerLoadSucces;
        bannerView.OnAdFailedToLoad += BannerLoadFailed;
        // Load a banner ad.
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);

    }
    private void BannerLoadSucces(object sender, EventArgs e)
    {
        bannerLoaded = true;
    }

    private void BannerLoadFailed(object sender, AdFailedToLoadEventArgs e)
    {
        bannerLoaded = false;
    }


#endif
    public void RequestInterstitial()
    {
#if USE_ADMOB
        #region request Admob
        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        // Create an interstitial.
        this.interstitial = new InterstitialAd(ID_FULL);
        // Register for ad events.

        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
        #endregion
#endif
    }
    #region Unity
#if USE_UNITY_ADS
    void HandleShowFullResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            RequestInterstitial();
            StartCoroutine(CompleteMethodInterstitial());
        }
        else if (result == ShowResult.Skipped)
        {
            RequestInterstitial();
            StartCoroutine(CompleteMethodInterstitial());
        }
        else if (result == ShowResult.Failed)
        {
            RequestInterstitial();
        }
    }
    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            RequestRewardBasedVideo();
            StartCoroutine(CompleteMethodRewardedVideo(true));
        }
        else if (result == ShowResult.Skipped)
        {
            RequestRewardBasedVideo();
            StartCoroutine(CompleteMethodRewardedVideo(false));
        }
        else if (result == ShowResult.Failed)
        {

            RequestRewardBasedVideo();
        }
    }

#endif
    #endregion

    private IEnumerator CompleteMethodInterstitial()
    {
        yield return null;
        if (OnInterstitialClosed != null)
        {
            OnInterstitialClosed();
            OnInterstitialClosed = null;
        }
    }
#if USE_ADMOB
    #region Interstitial callback handlers

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        // RequestInterstitial();
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
        StartCoroutine(CompleteMethodInterstitial());
    }



    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        // RequestInterstitial();
    }

    #endregion
#endif

#if USE_ADMOB

    #region RewardBasedVideo callback handlers
    private int maxRetryCount = 10;
    private int currentRetryRewardedVideo;
    IEnumerator CompleteMethodRewardedVideo(bool val)
    {
        yield return null;
        if (OnCompleteMethod != null)
        {
            OnCompleteMethod(val);
            OnCompleteMethod = null;
        }
    }
    private void RewardBasedVideo_OnAdLoaded(object sender, EventArgs e)
    {
        currentRetryRewardedVideo = 0;
    }
    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        if (currentRetryRewardedVideo < maxRetryCount)
        {
            currentRetryRewardedVideo++;
            RequestRewardBasedVideo();
        }
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        RequestRewardBasedVideo();
        if (triggerCompleteMethod)
        {
            StartCoroutine(CompleteMethodRewardedVideo(false));
        }
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        triggerCompleteMethod = false;
        RequestRewardBasedVideo();
        StartCoroutine(CompleteMethodRewardedVideo(true));

    }



    #endregion
#endif
    #endregion

}

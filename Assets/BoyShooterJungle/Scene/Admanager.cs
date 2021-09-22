using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;

public class Admanager : MonoBehaviour {
[Header("UnityAdd")]
     public string UnitygameId;
    public string myPlacementId;
    public static Admanager instance;

    
    
    private BannerView bannerView;
[Header("Admobe")]
   public string bannerID;

    private InterstitialAd fullScreenAd;
    public string fullScreenAdID;

    private RewardBasedVideoAd rewardedAd;
    public string rewardedAdID;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {


        RequestFullScreenAd();
        RequestBanner();

//           Advertisement.AddListener (this);
           Advertisement.Initialize (UnitygameId);
        rewardedAd = RewardBasedVideoAd.Instance;
     

        RequestRewardedAd();
		  // Called when an ad request has successfully loaded.
    this.fullScreenAd.OnAdLoaded += HandleOnAdLoaded;
    // Called when an ad request failed to load.
    this.fullScreenAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
    // Called when an ad is shown.
    this.fullScreenAd.OnAdOpening += HandleOnAdOpened;
    // Called when the ad is closed.
    this.fullScreenAd.OnAdClosed += HandleOnAdClosed;
    // Called when the ad click caused the user to leave the application.
   // this.fullScreenAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
         

        rewardedAd.OnAdLoaded += HandleRewardBasedVideoLoaded;

        rewardedAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;

        rewardedAd.OnAdRewarded += HandleRewardBasedVideoRewarded;

        rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;
    }


    public void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }
     public void HideIntersti()
    {
        fullScreenAd.Destroy();
    }

    public void RequestFullScreenAd()
    {
        fullScreenAd = new InterstitialAd(fullScreenAdID);

        AdRequest request = new AdRequest.Builder().Build();

        fullScreenAd.LoadAd(request);

    }

    public void ShowFullScreenAd()
    {
        if (fullScreenAd.IsLoaded())
        {
            fullScreenAd.Show();
        }else
        {
            Debug.Log("Full screen ad not loaded");
        }
    }

    public void RequestRewardedAd()
    {
        AdRequest request = new AdRequest.Builder().Build();

        rewardedAd.LoadAd(request, rewardedAdID);
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }else
        {
            Debug.Log("Rewarded ad not loaded");
        }
    }
      public void ShowRewardedVideoUni() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(myPlacementId)) {
            Advertisement.Show(myPlacementId);
        } 
        else {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }


public void HandleOnAdLoaded(object sender, EventArgs args)
{
    MonoBehaviour.print("HandleAdLoaded event received");
}

public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
{
    MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
}

public void HandleOnAdOpened(object sender, EventArgs args)
{
    MonoBehaviour.print("HandleAdOpened event received");
}

public void HandleOnAdClosed(object sender, EventArgs args)
{
    MonoBehaviour.print("HandleAdClosed event received");
    RequestFullScreenAd();
}

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        Debug.Log("Rewarded Video ad loaded successfully");

    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Failed to load rewarded video ad : " + args.Message);


    }



    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log("You have been rewarded with  " + amount.ToString() + " " + type);

       


    }


    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        Debug.Log ("Rewarded video has closed");
        RequestRewardedAd();

    }
}

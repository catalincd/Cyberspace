using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
	public Text debugText;
    private BannerView bannerView;

    public void Start()
    {
        #if UNITY_ANDROID
            string appId = "ca-app-pub-3940256099942544~3347511713";
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
        #else
            string appId = "unexpected_platform";
        #endif

        debugText.text = "Started";

        MobileAds.Initialize(appId);

        debugText.text = "Initialized";

        this.RequestBanner();
        this.ShowBanner();
    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif


        

        debugText.text = "Created";

        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;


        debugText.text = "WillReq";
        AdRequest request = new AdRequest.Builder().Build();
		debugText.text = "CreatedReq";
        this.bannerView.LoadAd(request);
		debugText.text = "Requested";
    }

    public void ShowBanner()
    {
    	debugText.text = "WillShow";
    	this.bannerView.Show();
    	debugText.text = "Showed";
    }

    public void HideBanner()
    {
    	this.bannerView.Hide();
    	RequestBanner();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        debugText.text = "LOADED SUCCESS";
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        debugText.text = "FAILED TO LOAD " + args.Message;
    }
}

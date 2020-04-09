using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;


public class AdReq : MonoBehaviour
{
    private BannerView bannerView;
    public Text refText;

    public string appId = "ca-app-pub-2645076787825256~5704277035";
    public string bannerId = "ca-app-pub-3940256099942544/6300978111";

    void Start()
    {
    	refText.text = "initialized";
        Debug.Log("AD INITIALIZED");
        MobileAds.Initialize(appId);
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
    	yield return new WaitForSeconds(2.0f);
    	refText.text = "requested";
        Debug.Log("AD REQUESTED");
        RequestBanner();
    }

    void Update()
    {
        
    }

    public void destroy()
    {
    	if(bannerView != null)
    	{
    		bannerView.Hide();
    		//bannerView.Destroy();
    	}
    }

    private void RequestBanner()
    {
        this.bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);

        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        bannerView.Show();
    }

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        refText.text = "LOADED";
        Debug.Log("AD LOADED");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    	refText.text = "FAILED: " + args.Message;
    	Debug.Log("FAILED TO LOAD AD");
    }
}

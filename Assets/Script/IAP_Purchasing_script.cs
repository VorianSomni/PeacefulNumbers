using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

[Serializable]
public class NonConsumableItem
{
    public string Name;
    public string Id;
    public string Description;
    public float price;
}


public class IAP_Purchasing_script : MonoBehaviour
{
    
    public AdsManager adsManager;
    public SudokuGame sudokuGame;

    public GameObject NoAdsPanel;
    public GameObject DarkScreenPanel;
    public string environment = "production";


    async void Start()
    {
        try
        {
            var options = new InitializationOptions().SetEnvironmentName(environment);

            await UnityServices.InitializeAsync(options);
        }
        catch (Exception)
        {
            print("An error occurred during initialization");
        }
    }

    public void RemoveAds()
    {
        sudokuGame.PlayerPurchasedNoAds = true; // Isso implica que o jogador comprou o 'No Ads'
        StartCoroutine(adsManager.IbuyedNoAdsSoTurnAdHelpButtonOff());
        sudokuGame.NoAdsButton.interactable = false;
        NoAdsPanel.SetActive(false);
        DarkScreenPanel.SetActive(false);
        sudokuGame.SaveConfig();
        adsManager.ShouldCallAds = false;
    }

    public void DontRemoveAds()
    {
        sudokuGame.PlayerPurchasedNoAds = false; // Isso implica que o jogador comprou o 'No Ads'
        
        NoAdsPanel.SetActive(false);
        DarkScreenPanel.SetActive(false);
        
        sudokuGame.SaveConfig();
        adsManager.ShouldCallAds = true;
    }
   
}



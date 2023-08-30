using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using TMPro;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public GameObject FrontGround;
    public GameObject ResetScreen;
    public GameObject NotFinishedScreen;
    public SudokuGame sudokuGame;

    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    
    [SerializeField] public byte TimesAdWasPlayed = 0;
    [SerializeField] TextMeshProUGUI AdTimesPlayedText;
    [SerializeField] TextMeshProUGUI AdText;
    [SerializeField] Button HelpAdButton;
    [SerializeField] CanvasGroup AdsCanvasGroup;
    [SerializeField] JsonSaving jsonSaving;

    private string _gameId;
    public string lastDate;

    private void Awake()
    {
        InitializeAds();
    }

    private void Start()
    {
        
    }

    public void LoadAds()
    {
        Advertisement.Load("RestartGame", this);
        Advertisement.Load("HelpAd", this);
        Advertisement.Load("WinAd", this);
    }

    
    public void ShowRestartAd()
    {
        Advertisement.Show("RestartGame", this);
    }

    public void ShowHelpAds()
    {
        Advertisement.Show("HelpAd", this);
    }

    public void ShowWinAd()
    {
        Advertisement.Show("WinAd", this);
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }
    public void OnInitializationComplete()
    {
        ShowMessage("Unity Ads initialization complete.");
        LoadAds();
        StartCoroutine(TurnAdHelpButtonOn());

    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        ShowMessage($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        ShowMessage("Unity Ads "+ placementId + " is loaded.");
    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        ShowMessage("Unity Ads " + placementId + " failed to load.");
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {

        ShowMessage("Unity Ads " + placementId + " failed to show.");
    }
    public void OnUnityAdsShowStart(string placementId)
    {
        ShowMessage("Unity Ads " + placementId + " did what?");
    }
    public void OnUnityAdsShowClick(string placementId)
    {
        ShowMessage("Unity Ads " + placementId + " did what?");
    }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(placementId == "HelpAd")
        {
            TimesAdWasPlayed++;

            UpdateBottomAdTexts();
            
            sudokuGame.SaveConfig();
        }

        if (placementId == "RestartGame")
        {
            FrontGround.SetActive(false);
            ResetScreen.SetActive(false);
            NotFinishedScreen.SetActive(false);
            sudokuGame.ResetGame();
        }

        if(placementId == "WinAd")
        {
            sudokuGame.Win();
        }
        
        LoadAds();
    }
    private void ShowMessage(string message)
    {
        print(message);
    }

    IEnumerator TurnAdHelpButtonOn()
    {
        yield return new WaitForSeconds(2);

        if(lastDate != System.DateTime.Today.ToString())
        {
            TimesAdWasPlayed = 0;
            UpdateBottomAdTexts();
            AdsCanvasGroup.gameObject.SetActive(true);
            LeanTween.alphaCanvas(AdsCanvasGroup, 1, 3).setEase(LeanTweenType.easeOutSine);
            yield return new WaitForSeconds(2);
        }
        else
        {
            TimesAdWasPlayed = (byte)jsonSaving.configSave.TimesAdWasPlayedToday;
            UpdateBottomAdTexts();
            if (TimesAdWasPlayed < 3)
            {
                AdsCanvasGroup.gameObject.SetActive(true);
                LeanTween.alphaCanvas(AdsCanvasGroup, 1, 3).setEase(LeanTweenType.easeOutSine);
                yield return new WaitForSeconds(2);
            }
        }
    }

    IEnumerator TurnAdHelpButtonOff()
    {
        yield return new WaitForSeconds(10);
        LeanTween.alphaCanvas(AdsCanvasGroup, 0, 2).setEase(LeanTweenType.easeOutSine);
        yield return new WaitForSeconds(2);
        HelpAdButton.gameObject.SetActive(false);
        AdsCanvasGroup.gameObject.SetActive(false);
    }

    public void UpdateBottomAdTexts()
    {
        if (sudokuGame.lang == 1)
        {
            AdText.text = "Help us grow!\n3 ads/day pay our coffee.";
            AdTimesPlayedText.text = TimesAdWasPlayed + "/3 ads\nseen today";
        }
        else
        {
            AdText.text = "Ajude-nos a crescer!\n3 ads/dia pagam nosso café.";
            AdTimesPlayedText.text = TimesAdWasPlayed + "/3 ads\nvistos hoje";
        }

        if(TimesAdWasPlayed == 3)
        {
            HelpAdButton.interactable = false;

            AdTimesPlayedText.text = "";
            if (sudokuGame.lang == 1)
            {
                AdText.text = "Thank you very much!\nMay your game be smooth\nand may your mind develop!";
            }
            else
            {
                AdText.text = "Muito obrigado mesmo!\nQue seu jogo seja tranquilo\ne que sua mente se desenvolva!";
            }
            StartCoroutine(TurnAdHelpButtonOff());
        }


    }

    
}

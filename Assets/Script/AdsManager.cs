using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Networking;
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
    [SerializeField] public bool ShouldCallAds = true;

    [SerializeField] public byte TimesAdWasPlayed = 0;
    [SerializeField] TextMeshProUGUI AdTimesPlayedText;
    [SerializeField] TextMeshProUGUI AdText;
    [SerializeField] Button HelpAdButton;
    [SerializeField] CanvasGroup AdsCanvasGroup;
    [SerializeField] CanvasGroup text1;
    [SerializeField] CanvasGroup text2;
    [SerializeField] JsonSaving jsonSaving;

    private string _gameId;
    public string lastDate;
    public bool hasInternetConnection;

    public string[] phrasesInternetOff_ENG;
    public string[] phrasesInternetOff_PT;

    public void StartThisScript()
    {
            StartCoroutine(TryInitializeAds());
            Debug.Log("After awake");
    }

    public void LoadAds()
    {
            Advertisement.Load("RestartGame", this);
            Advertisement.Load("HelpAd", this);
            Advertisement.Load("WinAd", this);
    }

    
    public void ShowRestartAd()
    {
        if (hasInternetConnection && ShouldCallAds)
        {
            Advertisement.Show("RestartGame", this);
        }
        else
        {
            FrontGround.SetActive(false);
            ResetScreen.SetActive(false);
            NotFinishedScreen.SetActive(false);
            sudokuGame.ResetGame();
        }
    }

    public void ShowHelpAds()
    {
        if (hasInternetConnection) 
        {
            Advertisement.Show("HelpAd", this);
        }
    }

    public void ShowWinAd()
    {
        if (hasInternetConnection)
        {
            Advertisement.Show("WinAd", this);
        }
        else if(!hasInternetConnection || !ShouldCallAds)
        {
            sudokuGame.Win();
        }
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
        if (!Advertisement.isInitialized && Advertisement.isSupported && ShouldCallAds)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAds();
        StartCoroutine(TurnAdHelpButtonOn());

    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log(message);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        ShowMessage("Unity Ads "+ placementId + " is loaded.");
    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log(message);
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log(message);
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
        if (hasInternetConnection)
        {
            if (placementId == "HelpAd")
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

            if (placementId == "WinAd")
            {
                sudokuGame.Win();
            }

            LoadAds();
        }
        else
        {
            if (placementId == "HelpAd")
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

            if (placementId == "WinAd")
            {
                sudokuGame.Win();
            }
        }
        
    }
    private void ShowMessage(string message)
    {
        Debug.Log(message);
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
        if (hasInternetConnection)
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
        }
        else
        {
            if (sudokuGame.lang == 1)
            {
                AdTimesPlayedText.text = "";
                AdText.text = "No internet connection? No problem. Thanks for playing our game!";
            }
            else
            {
                AdTimesPlayedText.text = "";
                AdText.text = "Está sem internet? Sem problema. Obrigado por jogar nosso jogo!";
            }
            
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
    
    IEnumerator BottomAdNoInternet()
    {
        AdTimesPlayedText.text = "";
        yield return new WaitForSeconds(10f);

        for (int i = 0; i < 100; i++)
        {
            Debug.Log("Esse foi o texto nº" + i);
            LeanTween.alphaCanvas(text1, 0, 1.5f);
            yield return new WaitForSeconds(1.5f);
            
            if (sudokuGame.lang == 1)
            {
                int value = UnityEngine.Random.Range(0, phrasesInternetOff_ENG.Length);
                AdText.text = phrasesInternetOff_ENG[value];
                LeanTween.alphaCanvas(text1, 1, 1.5f);
                yield return new WaitForSeconds(6.5f);
            }
            else
            {
                int value = UnityEngine.Random.Range(0, phrasesInternetOff_PT.Length);
                AdText.text = phrasesInternetOff_PT[value];
                LeanTween.alphaCanvas(text1, 1, 1.5f);
                yield return new WaitForSeconds(6.5f);
            }
           
        }

        yield return new WaitForSeconds(5);
        if (sudokuGame.lang == 1)
        {
            AdText.text = "A Cup of Bytes wishes you a great game!";
        }
        else
        {
            AdText.text = "A Cup of Bytes deseja a você um jogo maravilhoso!";
        }
        yield return new WaitForSeconds(5);
        StartCoroutine(TurnAdHelpButtonOff());
    }

    IEnumerator TryInitializeAds()
    {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("Essa foi a tentativa nº" + i);
                TestInternetConnection();
                yield return new WaitForSeconds(3);
                if (!hasInternetConnection)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            if (hasInternetConnection)
            {
                Debug.Log("Tem internet");
                InitializeAds();
            }
            else
            {
                Debug.Log("Não tem internet");
                StartCoroutine(TurnAdHelpButtonOn());
                StartCoroutine(BottomAdNoInternet());
                yield return null;
            }
        
    }

    public void TestInternetConnection()
    {
        StartCoroutine(TestInternet());
    }

    IEnumerator TestInternet()
    {
        UnityWebRequest request = new UnityWebRequest("https://duckduckgo.com");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            hasInternetConnection = false;
        }
        else
        {
            hasInternetConnection = true;
        }
    }


    public IEnumerator IbuyedNoAdsSoTurnAdHelpButtonOff()
    {
        HelpAdButton.interactable = false;

        AdTimesPlayedText.text = "";

        if (sudokuGame.lang == 1)
        {
            AdText.text = "Thanks for buying our game!";
        }
        else
        {
            AdText.text = "Obrigado por comprar nosso jogo!";
        }

        yield return new WaitForSeconds(10);
        LeanTween.alphaCanvas(AdsCanvasGroup, 0, 2).setEase(LeanTweenType.easeOutSine);
        yield return new WaitForSeconds(2);
        HelpAdButton.gameObject.SetActive(false);
        AdsCanvasGroup.gameObject.SetActive(false);
    }

}

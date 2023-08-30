using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressToPlay : MonoBehaviour
{
    public CanvasGroup SplashScreen;
    public CanvasGroup FlickTextCanvasGroup;
    public CanvasGroup TelaInicial;
    public BG_script BG_Script;
    public AdsManager adsManager;

    private void Start()
    {
        LeanTween.alphaCanvas(SplashScreen, 1, 3);
        StartCoroutine(FlickText());
        
    }


    IEnumerator FlickText()
    {
        LeanTween.alphaCanvas(FlickTextCanvasGroup, 1, 2).setLoopPingPong();
        yield return null;
    }


    public void LetThemPlay()
    {
        gameObject.GetComponent<Button>().interactable = false;
        LeanTween.cancel(FlickTextCanvasGroup.gameObject);
        StartCoroutine(C_LetThemPlay());
    }

    IEnumerator C_LetThemPlay()
    {
        BG_Script.gameObject.SetActive(true);
        BG_Script.StartBackground();

        LeanTween.alphaCanvas(FlickTextCanvasGroup, 0, 0.5f);
        yield return new WaitForSeconds(1);
        LeanTween.alphaCanvas(SplashScreen, 0, 3);
        LeanTween.alphaCanvas(TelaInicial, 1, 3);
        yield return new WaitForSeconds(3);
        adsManager.StartThisScript();
        gameObject.SetActive(false);
    }
}

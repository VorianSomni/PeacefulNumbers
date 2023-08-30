using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SS_rsc : MonoBehaviour
{
    public AudioListener audiolistener;
    public Image logo;
    public CanvasGroup Canvas;
    public AudioSource splashAudioSource;
    public AudioClip woohoo;
    public Slider LoadingBar;
    public TextMeshProUGUI LoadingText;
    public TextMeshProUGUI PercentageText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OpeningSequence());
    }

    IEnumerator OpeningSequence()
    {
        float size = 2.0f;
        LeanTween.scale(logo.gameObject, new Vector3(size, size, size), 1.5f);
        yield return new WaitForSeconds(1f);
        splashAudioSource.clip = woohoo;
        splashAudioSource.Play();
        yield return new WaitForSeconds(3f);
        LoadingBar.gameObject.SetActive(true);
        LoadingText.gameObject.SetActive(true);
        PercentageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        audiolistener.enabled = false;
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            asyncLoad.allowSceneActivation = true;
            LoadingBar.value = asyncLoad.progress;
            PercentageText.text = (int)(LoadingBar.value * 100) + "%";
            yield return new WaitForSeconds(0.001f);
            asyncLoad.allowSceneActivation = false;
        }
        LoadingBar.value = 1;
        PercentageText.text = "100%";
        yield return new WaitForSeconds(1f);
        LeanTween.alphaCanvas(Canvas, 0, 2);
        yield return new WaitForSeconds(2f);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("SudokuGame"));
        SceneManager.UnloadSceneAsync(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialHolder : MonoBehaviour
{
    public Sudoku_src sudoku_Src;
    public SudokuGame sudokugame;

    public TextMeshProUGUI titleHolder;
    public TextMeshProUGUI textHolder;
    public Tutorial[] tutorials;
    public Image imageHolder;

    [SerializeField] CanvasGroup tutorialPanel;
    [SerializeField] Button PreviousPageButtonMenu;
    [SerializeField] Button NextPageButtonMenu;
    [SerializeField] Button PreviousPageButton;
    [SerializeField] Button NextPageButton;
<<<<<<< HEAD:PeacefulNumbers/Assets/Script/TutorialHolder.cs
    [SerializeField] GameObject[] PanelHolder;

    int pageMenuTutorial = 0;
=======
>>>>>>> parent of 54cfe34e (Ver0.9):My project/Assets/Script/TutorialHolder.cs

    int Totalpages = 0;
    [SerializeField] int Actualpage = 0;
    int chosenTutorial = 0;

<<<<<<< HEAD:PeacefulNumbers/Assets/Script/TutorialHolder.cs
=======
    // Tutorial[0][Actualpage].ENtutorialImage;
    
>>>>>>> parent of 54cfe34e (Ver0.9):My project/Assets/Script/TutorialHolder.cs
    public void ChooseTutorial(int tutorial)
    {
        chosenTutorial = tutorial;

        if(sudokugame.lang == 1)
        {
            titleHolder.text = tutorials[chosenTutorial].ENName;
        }
        else
        {
            titleHolder.text = tutorials[chosenTutorial].PTName;
        }

        sudoku_Src.FadePanel(true, tutorialPanel);
        
        Totalpages = tutorials[chosenTutorial].PTtutorialImage.Length;
        Actualpage = 0;
        
        Updatepage();
        UpdateButtons();
    }

    public void NextPage()
    {
        if (Actualpage < Totalpages-1)
        {
            Actualpage++;
        }

        Updatepage();
        UpdateButtons();
    }

    public void PreviousPage()
    {
        if (Actualpage > 0)
        {
            Actualpage--;
        }

        Updatepage();
        UpdateButtons();
    }

    void Updatepage()
    {
        if (sudokugame.lang == 1)
        {
            imageHolder.sprite = tutorials[chosenTutorial].ENtutorialImage[Actualpage];
            textHolder.text = tutorials[chosenTutorial].ENtutorialText[Actualpage];
        }
        else
        {
            imageHolder.sprite = tutorials[chosenTutorial].PTtutorialImage[Actualpage];
            textHolder.text = tutorials[chosenTutorial].PTtutorialText[Actualpage];
        }
        imageHolder.preserveAspect = true;
    }

    public void PanelFadeOut()
    {
        sudoku_Src.FadePanel(false, tutorialPanel);
    }

    void UpdateButtons()
    {
        PreviousPageButton.interactable = true;
        NextPageButton.interactable = true;

        if (Actualpage > 0 && Actualpage < Totalpages-1)
        {
            PreviousPageButton.interactable = true;
            NextPageButton.interactable = true;
        }
        else if(Actualpage == 0)
        {
            PreviousPageButton.interactable = false;
        }
        else if(Actualpage == Totalpages - 1)
        {
            NextPageButton.interactable = false;
        }
        
    }
<<<<<<< HEAD:PeacefulNumbers/Assets/Script/TutorialHolder.cs

    // Tutorial menu buttons

    public void NextPageMenu()
    {
        if (pageMenuTutorial < 2)
            pageMenuTutorial++;

        if (pageMenuTutorial == 2)
            NextPageButtonMenu.interactable = false;
        else
            NextPageButtonMenu.interactable = true;

        if (pageMenuTutorial == 0)
            PreviousPageButtonMenu.interactable = false;
        else
            PreviousPageButtonMenu.interactable = true;

        for (int i = 0; i < PanelHolder.Length; i++)
        {
            PanelHolder[i].SetActive(false);
        }
        
        PanelHolder[pageMenuTutorial].SetActive(true);
    }

    public void PreviousPageMenu()
    {
        if (pageMenuTutorial > 0)
            pageMenuTutorial--;

        if (pageMenuTutorial == 0)
            PreviousPageButtonMenu.interactable = false;
        else
            PreviousPageButtonMenu.interactable = true;

        if (pageMenuTutorial == 2)
            NextPageButtonMenu.interactable = false;
        else
            NextPageButtonMenu.interactable = true;

        for (int i = 0; i < PanelHolder.Length; i++)
        {
            PanelHolder[i].SetActive(false);
        }

        PanelHolder[pageMenuTutorial].SetActive(true);
    }
=======
>>>>>>> parent of 54cfe34e (Ver0.9):My project/Assets/Script/TutorialHolder.cs
}

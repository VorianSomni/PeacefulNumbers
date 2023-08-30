using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NumberButtons : MonoBehaviour
{
    public Sudoku_src sudoku_Src;
    public GridSquare gridSquare;
    public SudokuGame sudokuGame;
    public bool isPencilActivated = false;

    private void Start()
    {
        sudoku_Src = FindObjectOfType<Sudoku_src>();
        sudokuGame = FindObjectOfType<SudokuGame>();
    }

    public void SetNumber(int number)
    {
        PencilTexts pencilTexts = null;

        try
        {
            pencilTexts = gridSquare.GetComponent<PencilTexts>();
        }
        catch (NullReferenceException)
        {
            return;
        }
        
        isPencilActivated = sudokuGame.isPencilActivated;

        if (gridSquare != null)
        {

            if (isPencilActivated & gridSquare != null & gridSquare.CanChangeNubers & sudokuGame.selectedSquare != null & gridSquare)
            {
                if (sudokuGame.selectedSquare.text != " ")
                {
                    sudokuGame.selectedSquare.text = " ";
                }

                foreach (var item in pencilTexts.penciltexts)
                {
                    TextMeshProUGUI textMeshProUGUI = item.GetComponent<TextMeshProUGUI>();
                    if (item.name == $"Pencil_text ({number})" & textMeshProUGUI.enabled == false)
                    {
                        textMeshProUGUI.enabled = true;
                    }
                    else if (item.name == $"Pencil_text ({number})" & textMeshProUGUI.enabled == true)
                    {
                        item.GetComponent<TextMeshProUGUI>().enabled = false;
                    }
                }
            }
        }
        
        
        


        if (gridSquare != null & gridSquare.CanChangeNubers & sudokuGame.selectedSquare != null & gridSquare & sudokuGame.selectedSquare.text == " " & !isPencilActivated)
        {
            // Esse daqui pega todos os lapís dentro desse quadrado e desliga.
            foreach (var item in pencilTexts.penciltexts)
            {
                item.GetComponent<TextMeshProUGUI>().enabled = false;
            }

            // E então seta que o número é aquele.
            gridSquare.gameObject.SetActive(true);
            sudokuGame.selectedSquare.text = number.ToString();
            gridSquare.ExternalHighlight();

            // Preciso de um código que pegue todos os quadrados dentro da linha, coluna e grid9x9 e apague o lápis correspondente ao número.
            sudoku_Src.ErasePencil(gridSquare.rowsquares, number);
            sudoku_Src.ErasePencil(gridSquare.columnquares, number);
            sudoku_Src.ErasePencil(gridSquare.grid9x9squares, number);
        }

        sudokuGame.CheckSolution();
    }



    public void SetPen(GameObject gameObject)
    {
        isPencilActivated = !isPencilActivated;
        sudoku_Src.PencilColor(gameObject, isPencilActivated);
    }
}

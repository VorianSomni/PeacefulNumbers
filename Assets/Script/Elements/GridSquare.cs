using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GridSquare : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI number_text;
    public Sudoku_src sudoku_src;
    public SudokuGame sudokugame;
    private NumberButtons[] numberButtons;
    private ToolButtons[] ToolButtons;
    
    public Color ClearBlue = new Color(203f/255f, 233/255f, 245/255f, 255/255f);
    public Color SlightlyDarkerBlue = new Color(130f / 255f, 181 / 255f, 277 / 255f, 255 / 255f);
    public Color Selected = new Color(128f / 255f, 212 / 255f, 245 / 255f, 255 / 255f);
    public Color AquaGreen = new Color(42f / 255f, 160 / 255f, 137 / 255f, 255 / 255f);
    public Color Sgray = new Color(50f / 255f, 50 / 255f, 50 / 255f, 255 / 255f);
    
    public GameObject[] rowsquares = new GameObject[9];
    public GameObject[] columnquares = new GameObject[9];
    public GameObject[,] grid9x9squares = new GameObject[3, 3];

    public bool CanChangeNubers = false;
    
    int i = 0;
    int j = 0;


    private void Start()
    {
        sudoku_src = FindObjectOfType<Sudoku_src>();
        sudokugame = FindObjectOfType<SudokuGame>();
        numberButtons = FindObjectsOfType<NumberButtons>();
        ToolButtons = FindObjectsOfType<ToolButtons>();
    }

    public void DisplayText(int number)
    {
        if (number == 0)
        {
            number_text.text = " ";
            CanChangeNubers = true;
            number_text.color = AquaGreen;
        }
        else
        {
            number_text.text = number.ToString();
            CanChangeNubers = false;
            number_text.color = Sgray;
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        sudokugame.PlaySFX(1);
        sudokugame.gridSquare = this;
        // Setar todos os quadrados para branco e as fontes para Normal
        sudoku_src.ResetSquare(sudokugame.AxA_squares);

        // Pegar o endereço desse quadrado
        string pos = sudokugame.FindArrayPos(name);
        j = Convert.ToInt16(Char.GetNumericValue(pos[0]));
        i = Convert.ToInt16(Char.GetNumericValue(pos[1]));
        
        // Iluminar a linha, a coluna e o grid9x9
        if(gameObject.GetComponent<Button>().interactable != false)
        {
            Highlight(i, j);
        }

        sudokugame.selectedSquare = number_text;
        foreach (var numberButton in numberButtons)
        {
            numberButton.gridSquare = this;
        }

        foreach (var toolButtons in ToolButtons)
        {
            toolButtons.gridSquare = this;
        }
    }

    private void Highlight(int row, int col)
    {
        HighlightRow(col);
        HighlightColumn(row);
        Highlight9x9Grid(col, row);
        HighlightNumbers();
        HighlightCell();
    }

    public void ExternalHighlight()
    {
        HighlightNumbers();
        HighlightCell();
    }

    public void EraseHighlight()
    {
        sudoku_src.ResetSquare(sudokugame.AxA_squares);
        Highlight(i, j);
        UnHighlightNumbers();
    }

    private void HighlightCell()
    {
        if(CanChangeNubers)
            gameObject.GetComponent<Image>().color = Selected;
    }

    private void HighlightRow(int row)
    {
        
        for (int col = 0; col < 9; col++)
        {
            rowsquares[col] = sudokugame.AxA_squares[row, col];
        }

        foreach (var item in rowsquares)
        {
            item.gameObject.GetComponent<Image>().color = ClearBlue;
        }
    }

    private void HighlightColumn(int col)
    {
        
        for (int row = 0; row < 9; row++)
        {
            columnquares[row] = sudokugame.AxA_squares[row, col];
        }

        foreach (var item in columnquares)
        {
            item.gameObject.GetComponent<Image>().color = ClearBlue;
        }
        
    }

    private void Highlight9x9Grid(int row, int col)
    {
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                grid9x9squares[i,j] = sudokugame.AxA_squares[3 * (row / 3) + i, 3 * (col / 3) + j];
            }
        }
        
        foreach (var item in grid9x9squares)
        {
            item.gameObject.GetComponent<Image>().color = ClearBlue;
        }

    }

    private void HighlightNumbers()
    {
        int count = 0;
        GameObject[] equalNumberSquares = new GameObject[81];
        if (number_text.text != " " & number_text.text != "0")
        {
            foreach (var square in sudokugame.AxA_squares)
            {
                if(square.GetComponent<GridSquare>().number_text.text == number_text.text)
                {
                    equalNumberSquares[count] = square;
                    count++;
                }
            }
        }

        foreach (var item in equalNumberSquares)
        {
            if(item != null)
            {
                item.gameObject.GetComponent<Image>().color = SlightlyDarkerBlue;
                item.GetComponent<GridSquare>().number_text.fontStyle = FontStyles.Bold;
            }
            
        }

    }

    private void UnHighlightNumbers()
    {
        foreach (var number in sudokugame.AxA_squares)
        {
            number.GetComponent<GridSquare>().number_text.fontStyle = FontStyles.Normal;
        }
    }

}


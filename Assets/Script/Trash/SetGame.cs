using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
/// Esse script vai pegar um arquivo de texto com strings de jogos sudoku
/// E vai setar isso no board para o jogador.
*/

[System.Serializable]
public class SetGame : MonoBehaviour
{
    public SolutionChecker SolutionChecker;
    public CanvasGroup panel;
    public Sudoku_src sudoku_src;
    public GameObject[] squares;
    public GameObject[,] AxA_squares = new GameObject[9, 9];
    public TextMeshProUGUI selectedSquare;
    int[,] gameGrid = new int[9,9];

    private void Start()
    {
        sudoku_src = FindObjectOfType<Sudoku_src>();

        // Create an Array of Arrays with the Squares GameObjects
        int count = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                AxA_squares[i, j] = squares[count];
                count++;
            }
        }

        EnableButtons(false);
    }


    public void CreateGame(int difficulty)
    {
        
        int[,] grid = { { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

        sudoku_src.GenerateSolvedSudoku(grid);
        SolutionChecker.Solution = grid;
        gameGrid = sudoku_src.CreateGameByDificulty(grid, difficulty);
        SolutionChecker.Game = gameGrid;
        EnableButtons(false);
        StartCoroutine(SetGameOnSquares());
    }

    IEnumerator SetGameOnSquares()
    {
        sudoku_src.FadePanel(true, panel);
        yield return new WaitForSeconds(1f);

        sudoku_src.ResetSquare(AxA_squares);
        sudoku_src.ResetFont(AxA_squares);
        sudoku_src.EraseAllPencil(AxA_squares);
        int count = 0;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GridSquare text_square = AxA_squares[i, j].GetComponent<GridSquare>();
                text_square.DisplayText(SolutionChecker.Game[i, j]);
                count++;

            }
        }

        sudoku_src.FadePanel(false, panel);
        EnableButtons(true);
        yield return null;
    }

    public string FindArrayPos(string name)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (AxA_squares[i,j].name == name)
                {
                    string pos = $"{i}{j}";
                    return pos;
                }
            }
        }
        return null;
    }

    private void EnableButtons(bool interactable)
    {
        foreach (var item in AxA_squares)
        {
            item.GetComponent<Button>().interactable = interactable;
        }
    }

    public void ResetGame()
    {
        StartCoroutine(SetGameOnSquares());
    }
}

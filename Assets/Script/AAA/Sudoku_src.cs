using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Sudoku_src : MonoBehaviour
{
    System.Random rand = new System.Random();
    public int[,] board2 = new int[9, 9];
    Color Sgray = new Color(50f / 255f, 50 / 255f, 50 / 255f, 255 / 255f);
    
    public bool GenerateSolvedSudoku(int[,] board)
    {

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (board[i,j] == 0)
                {
                    List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    int num = nums[rand.Next(nums.Count)];
                    for (int n = 1; n < 10; n++)
                    {
                        if (IsValid(board, num, i, j))
                        {
                            board[i,j] = num;
                            if (!GenerateSolvedSudoku(board))
                            {
                                board[i,j] = 0;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            nums.Remove(num);
                        }
                    }
                    return false;
                }
            }
        }
        return true;
    }

    bool IsValid(int[,] board, int num, int row, int col)
    {
        for (int i = 0; i < 9; i++)
        {
            //check row  
            if (board[i, col] != 0 && board[i, col] == num)
                return false;
            //check column  
            if (board[row, i] != 0 && board[row, i] == num)
                return false;
            //check 3*3 block  
            if (board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] != 0 && board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] == num)
                return false;
        }
        return true;

    }

    public string CreateString(int[,] matriz)
    {
        var text = new System.Text.StringBuilder();
        foreach (var item in matriz)
        {
            text.Append(item.ToString());
        }
        return text.ToString();
    }

    public int[,] CreateMatriz(string game)
    {
        List<char> chars = new List<char>();
        for (int i = 0; i < game.Length; i++)
        {
            chars.Add(game[i]);
        }

        int[,] ints = new int[9, 9];
        int count = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                ints[i, j] = int.Parse(chars[count].ToString());
                count++;
            }
            
        }
        return ints;
    }

    public int[,] CreateGameByDificulty(int[,] matriz, int difficulty)
    {
        int random_num = 0;
        int[,] matriz_diff = new int[9, 9];
        Array.Copy(matriz, matriz_diff, matriz.Length);

        if (difficulty == 5)   // Evil
        {
            random_num = rand.Next(56, 58);
        }

        if (difficulty == 4)   // Expert
        {
            random_num = rand.Next(46, 55);
        }

        if (difficulty == 3)   // Hard
        {
            random_num = rand.Next(40, 45);
        }
        
        if (difficulty == 2)   // Medium
        {
            random_num = rand.Next(33, 39);
        }
        
        if (difficulty == 1)    // Easy
        {
            random_num = rand.Next(25, 32);
        }
        
        if (difficulty == 0)    // Novice
        {
            random_num = rand.Next(18, 24);
        }

        StartCoroutine(ZerosOnGrid(random_num, matriz_diff, matriz));


        return matriz_diff;
    }

    public bool VerifySolution(int[,] matrizOriginal, int[,] matrizJogo)
    {
        int[,] unsolvedSolution = matrizJogo;

        if(solver(unsolvedSolution, 0) > 1)
        {
            return false;
        }
        
        if(unsolvedSolution == matrizOriginal)
        {
            return true;
        }
        return false;
    }

    public int solver(int[,] board, int count)
    { // Starts with count = 0

        for (int i = 0; i < 9; i++)
        { //GRID_SIZE = 9

            for (int j = 0; j < 9; j++)
            {

                /*
                 * Only empty fields will be changed
                 */

                if (board[i,j] == 0)
                { //EMPTY = 0

                    /*
                     * Try all numbers between 1 and 9
                     */

                    for (int n = 1; n <= 9 && count < 2; n++)
                    {

                        /*
                         * Is number n safe?
                         */
                        if (IsValid(board, n, i, j))
                        {

                            board[i,j] = n;
                            int cache = solver(board, count);
                            if (cache > count)
                            {
                                count = cache;
                                for (int k = 0; k < 9; k++)
                                {
                                    for (int l = 0; l < 9; l++)
                                    {
                                        if (board[k,l] != 0)
                                        {
                                            board2[k,l] = board[k,l];
                                        }

                                    }
                                }

                                board[i,j] = 0;

                            }
                            else
                            {
                                board[i,j] = 0;
                            }

                        }
                    }
                    return count;
                }
            }
        }
        return count + 1;
    }

    IEnumerator ZerosOnGrid(int random_num, int[,] matriz_diff, int[,] matriz)
    {
        int pos_x;
        int pos_y;
        int tries = 0;


        for (int i = 0; i < random_num; i++)
        {
            if(tries > 200)
            {
                break;
            }

            while (true)
            {
                pos_x = rand.Next(0, 9);
                pos_y = rand.Next(0, 9);
                if (matriz_diff[pos_x, pos_y] != 0)
                {
                    matriz_diff[pos_x, pos_y] = 0;
                    break;
                }
            }

            if (solver(matriz_diff, 0) > 1)
            {
                matriz_diff[pos_x, pos_y] = matriz[pos_x, pos_y];
                i--;
                tries++;
                continue;
            }

            tries = 0;
        }

        yield return matriz_diff;
    }

    private void CountZeros(int[,] matriz)
    {
        int count = 0;
        foreach (var item in matriz)
        {
            if(item == 0)
            {
                count++;
            }
        }
        print(count);
    }

    public void ResetSquare(GameObject[,] grid)
    {
        foreach (var item in grid)
        {
            item.gameObject.GetComponent<Image>().color = Color.white;
            item.GetComponent<GridSquare>().number_text.fontStyle = FontStyles.Normal;
        }
    }

    public void ResetFont(GameObject[,] grid)
    {
        foreach (var item in grid)
        {
            item.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Sgray;
        }
    }

    public void FadePanel(bool in_out, CanvasGroup panel)
    {
        StartCoroutine(Fade(in_out, panel));
    }

    IEnumerator Fade(bool in_out, CanvasGroup panel)
    {
        if (in_out == true)
        {
            panel.interactable = true;
            panel.gameObject.SetActive(true);
            LeanTween.alphaCanvas(panel, 1, 0.5f);
        }
        else
        {
            panel.interactable = false;
            LeanTween.alphaCanvas(panel, 0, 0.5f);
            yield return new WaitForSeconds(0.6f);
            panel.gameObject.SetActive(false);
        }
        yield return null;
    }

    public void PencilColor(GameObject button, bool state)
    {
        if (state)
            button.GetComponent<Image>().color = Color.red;
        else
            button.GetComponent<Image>().color = Color.white;
    }

    public void ErasePencil(GameObject[] squares, int number)
    {
        foreach (var item in squares)
        {
            TextMeshProUGUI[] penciltexts = item.GetComponent<PencilTexts>().penciltexts;
            foreach (var penciltext in penciltexts)
            {
                if (penciltext.name == $"Pencil_text ({number})")
                {
                    penciltext.enabled = false;
                }
            }
        }
    }

    public void ErasePencil(GameObject[,] squares, int number)
    {
        foreach (var item in squares)
        {
            TextMeshProUGUI[] penciltexts = item.GetComponent<PencilTexts>().penciltexts;
            foreach (var penciltext in penciltexts)
            {
                if (penciltext.name == $"Pencil_text ({number})")
                {
                    penciltext.enabled = false;
                }
            }
        }
    }

    public void EraseAllPencil(GameObject[,] squares)
    {
        foreach (var item in squares)
        {
            TextMeshProUGUI[] penciltexts = item.GetComponent<PencilTexts>().penciltexts;
            foreach (var penciltext in penciltexts)
            {
                penciltext.enabled = false;
            }
        }
    }

    public string GetDifferenceGame(GameObject[] squares, int[,] game)
    {
        // squares = game + fill
        // game = game

        var filledgame = new System.Text.StringBuilder();
        foreach (var item in squares)
        {
            if (item.gameObject.GetComponent<GridSquare>().number_text.text == " ")
                filledgame.Append("0");
            else
                filledgame.Append(item.gameObject.GetComponent<GridSquare>().number_text.text);
        }

        string nofillgame = CreateString(game);

        // Compare both strings and do magic *-*

        var stuff = new System.Text.StringBuilder();
        for (int i = 0; i < filledgame.Length; i++)
        {
            if (nofillgame[i] == filledgame[i])
            {
                stuff.Append("0");
            }
            else
            {
                stuff.Append(filledgame[i]);
            }
        }

        return stuff.ToString();
    }


}

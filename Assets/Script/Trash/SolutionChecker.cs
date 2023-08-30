using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SolutionChecker : MonoBehaviour
{
    public Sudoku_src Resources;
    public SetGame SetGame;
    public int[,] Solution;
    public int[,] Game;

    public void CheckSolution()
    {
        var GivenSolution = new System.Text.StringBuilder();
        foreach (var item in SetGame.AxA_squares)
        {
            GridSquare grid = item.GetComponent<GridSquare>();
            if(grid.number_text.text == " ")
            {
                GivenSolution.Append("0");
            }
            else
            {
                GivenSolution.Append(grid.number_text.text);
            }
        }

        // Verifique a solução! hehehe
        if(Resources.CreateString(Solution) == GivenSolution.ToString())
        {
            Win();
        }
        
    }


    public void Win()
    {
        // abrir a tela falando que o jogo finalizou, parabenizar o jogador e perguntar se ele quer jogar um jogo novo.
    }

}

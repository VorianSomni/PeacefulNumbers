using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ToolButtons : MonoBehaviour
{
    public Sudoku_src sudoku_Src;
    public GridSquare gridSquare;
    public SetGame setGame;
    

    public void EraseNumber()
    {
        if (gridSquare != null & gridSquare.CanChangeNubers & setGame.selectedSquare != null & gridSquare & setGame.selectedSquare.text != " ")
        {
            setGame.selectedSquare.text = " ";
            gridSquare.EraseHighlight();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChecker : MonoBehaviour
{
    public List<Square> squareList = new();
    public ScoreController scoreController;

    public void CheckLine()
    {
        if (GetComponent<SpriteRenderer>().color != ColorData.clearColor) return;
        foreach(Color color in new[] { ColorData.myColor, ColorData.opponentColor })
        {
            if (CheckLineColor(color))
            {
                GetComponent<SpriteRenderer>().color = color;
                scoreController.ScoreLine(color);
                return;
            }
        }
    }

    private bool CheckLineColor(Color color)
    {
        foreach (Square square in squareList)
        {
            if (square.GetColor() != color) return false;
        }
        return true;
    }

    public void Reset()
    {
        GetComponent<SpriteRenderer>().color = ColorData.clearColor;
    }
}

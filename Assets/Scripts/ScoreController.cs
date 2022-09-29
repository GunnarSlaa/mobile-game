using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] myScoreFields = new SpriteRenderer[3];
    [SerializeField] private SpriteRenderer[] opponentScoreFields = new SpriteRenderer[3];
    [SerializeField] private SpriteRenderer[] roundScoreFields = new SpriteRenderer[3];

    [SerializeField] private SquareGrid squareGrid;
    [SerializeField] private PopUp popUp;

    private int myScore = 0;
    private int opponentScore = 0;
    private int myRoundsWon = 0;
    private int opponentRoundsWon = 0;
    private int roundCounter = 0;

    public void ScoreLine(Color color)
    {
        if (color == ColorData.myColor)
        {
            myScoreFields[myScore].color = color;
            myScore++;
            if (myScore == 3) { ScoreRound(color); }
        }
        else if (color == ColorData.opponentColor)
        {
            opponentScoreFields[opponentScore].color = color;
            opponentScore++;
            if (opponentScore == 3) { ScoreRound(color); }
        }
        else throw new System.Exception($"Wrong color sent to ScoreController: {color}");
    }

    void ScoreRound(Color color)
    {
        roundScoreFields[roundCounter].color = color;
        roundCounter++;
        foreach (SpriteRenderer scoreField in myScoreFields) { scoreField.color = ColorData.clearColor; }
        foreach (SpriteRenderer scoreField in opponentScoreFields) { scoreField.color = ColorData.clearColor; }
        myScore = 0;
        opponentScore = 0;
        squareGrid.ResetGrid();
        if (color == ColorData.myColor) myRoundsWon++;
        else if (color == ColorData.opponentColor) opponentRoundsWon++;
        else throw new System.Exception($"Wrong color sent to ScoreController: {color}");
        if (myRoundsWon >= 2)
        {
            popUp.Show("Game won!", false);
            return;
        }
        else if (opponentRoundsWon >= 2)
        {
            popUp.Show("Game lost!", false);
            return;
        }
        if (color == ColorData.myColor) popUp.Show("Round won!", true);
        else if (color == ColorData.opponentColor) popUp.Show("Round lost!", true);
        else throw new System.Exception($"Wrong color sent to ScoreController: {color}");
    }
}

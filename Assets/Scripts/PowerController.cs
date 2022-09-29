using Photon.Realtime;
using System;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    private Power powerSelected;
    private int powerNumber;

    [SerializeField] private EventController eventController;
    [SerializeField] private SquareGrid squareGrid;
    [SerializeField] private Deck deck;

    public void SelectPower(Power power, int powerNumber)
    {
        powerSelected = power;
        power.powerController = this;
        this.powerNumber = powerNumber;
    }

    public bool ExecutePower(Vector2Int gridPosition, bool temp = false)
    {
        if (!powerSelected) return false;
        Debug.Log("Temp power execution");
        powerSelected.Execute(gridPosition, temp);
        if (temp) return true;
        powerSelected = null;
        deck.ReplacePower(powerNumber);
        return true;
    }

    public void ColorSquare(Vector2Int gridPosition, Color color, bool temp)
    {
        //Color the square on own board
        squareGrid.ColorSquare(gridPosition, color, temp);
        if (temp) return;
        //Communicate coloring over network
        eventController.SendEvent(Constants.CLAIM_SQUARE_CODE, 
            new object[] { (Vector2)gridPosition, ColorData.ColorName(color) }, 
            ReceiverGroup.Others);
    }

    public void ClaimSquare(Vector2Int gridPosition, bool temp)
    {
        ColorSquare(gridPosition, ColorData.myColor, temp);
    }

    public void FlipSquare(Vector2Int gridPosition, bool temp)
    {
        squareGrid.FlipSquare(gridPosition, ColorData.myColor, ColorData.opponentColor, temp);
    }

    public void LockSquare(Vector2Int gridPosition, bool temp)
    {
        //Lock the square on own board
        squareGrid.LockSquare(gridPosition, temp);
        if (temp) return;
        //Communicate locking over network
        eventController.SendEvent(Constants.LOCK_SQUARE_CODE,
            new object[] { (Vector2)gridPosition },
            ReceiverGroup.Others);
    }

    public void ClearSquare(Vector2Int gridPosition, bool temp)
    {
        //Lock the square on own board
        squareGrid.ColorSquare(gridPosition, ColorData.clearColor, temp);
        if (temp) return;
        //Communicate clearing over network
        eventController.SendEvent(Constants.CLEAR_SQUARE_CODE,
            new object[] { (Vector2)gridPosition },
            ReceiverGroup.Others);
    }

    public delegate void ActionDelegate(Vector2Int gridPosition, bool temp);

    public void IfExists(ActionDelegate Action, Vector2Int gridPosition, bool temp)
    {
        if (squareGrid.SquareExists(gridPosition)) Action(gridPosition, temp);
    }

    public void NeighbourIfExists(ActionDelegate Action, Vector2Int gridPosition, int xDelta, int yDelta, bool temp)
    {
        Vector2Int neighbourPosition = squareGrid.GetNeighbour(gridPosition, xDelta, yDelta);
        IfExists(Action, neighbourPosition, temp);
    }
}

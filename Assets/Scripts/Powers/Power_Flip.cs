using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Flip : Power
{
    public override void Execute(Vector2Int gridPosition, bool temp)
    {
        powerController.FlipSquare(gridPosition, temp);
        powerController.NeighbourIfExists(powerController.FlipSquare, gridPosition, 0, -1, temp);
        powerController.NeighbourIfExists(powerController.FlipSquare, gridPosition, 1, 0, temp);
        powerController.NeighbourIfExists(powerController.FlipSquare, gridPosition, 0, 1, temp);
        powerController.NeighbourIfExists(powerController.FlipSquare, gridPosition, -1, 0, temp);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Clear : Power
{
    public override void Execute(Vector2Int gridPosition, bool temp)
    {
        powerController.ClearSquare(gridPosition, temp);
        powerController.NeighbourIfExists(powerController.ClearSquare, gridPosition, -2, 0, temp);
        powerController.NeighbourIfExists(powerController.ClearSquare, gridPosition, -1, 0, temp);
        powerController.NeighbourIfExists(powerController.ClearSquare, gridPosition, 1, 0, temp);
        powerController.NeighbourIfExists(powerController.ClearSquare, gridPosition, 2, 0, temp);
    }
}

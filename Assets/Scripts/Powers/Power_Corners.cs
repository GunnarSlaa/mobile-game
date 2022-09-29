using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Corners : Power
{
    public override void Execute(Vector2Int gridPosition, bool temp)
    {
        powerController.NeighbourIfExists(powerController.ClaimSquare, gridPosition, 1, -1, temp);
        powerController.NeighbourIfExists(powerController.ClaimSquare, gridPosition, 1, 1, temp);
        powerController.NeighbourIfExists(powerController.ClaimSquare, gridPosition, -1, 1, temp);
        powerController.NeighbourIfExists(powerController.ClaimSquare, gridPosition, -1, -1, temp);
    }
}

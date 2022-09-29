using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Lock : Power
{
    public override void Execute(Vector2Int gridPosition, bool temp)
    {
        powerController.LockSquare(gridPosition, temp);
        powerController.NeighbourIfExists(powerController.LockSquare, gridPosition, 0, -1, temp);
        powerController.NeighbourIfExists(powerController.LockSquare, gridPosition, 0, 1, temp);
    }
}

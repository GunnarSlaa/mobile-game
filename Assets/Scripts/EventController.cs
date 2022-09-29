using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class EventController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private SquareGrid squareGrid;

    public void OnEvent(EventData eventData)
    {
        if (eventData.Code == Constants.CLAIM_SQUARE_CODE)
        {
            object[] data = (object[])eventData.CustomData;
            Vector2Int gridPosition = Vector2Int.RoundToInt((Vector2)data[0]);
            Color color = ColorData.ColorFromString((string)data[1]);
            squareGrid.ColorSquare(gridPosition, ColorData.OppositeColor(color), false);
        }
        else if (eventData.Code == Constants.LOCK_SQUARE_CODE)
        {
            object[] data = (object[])eventData.CustomData;
            Vector2Int gridPosition = Vector2Int.RoundToInt((Vector2)data[0]);
            squareGrid.LockSquare(gridPosition, false);
        }
        else if (eventData.Code == Constants.CLEAR_SQUARE_CODE)
        {
            object[] data = (object[])eventData.CustomData;
            Vector2Int gridPosition = Vector2Int.RoundToInt((Vector2)data[0]);
            squareGrid.ColorSquare(gridPosition, ColorData.clearColor, false);
        }
    }

    public void SendEvent(byte code, object[] content, ReceiverGroup receiverGroup)
    {
        PhotonNetwork.RaiseEvent(code, content, new RaiseEventOptions { Receivers = receiverGroup }, SendOptions.SendReliable);
    }
}

using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private SquareGrid squareGrid;

    public bool myTurn = false;

    private int playerNumber;
    private int otherPlayerNumber;

    public void Start()
    {
        playerNumber = PhotonNetwork.CurrentRoom.Players[1] == PhotonNetwork.LocalPlayer ? 1 : 2;
        otherPlayerNumber = PhotonNetwork.CurrentRoom.Players[1] == PhotonNetwork.LocalPlayer ? 2 : 1;

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            turnText.text = "Waiting for 2nd player";
        }
        else if (playerNumber == 2)
        {
            //Determine starting player
            int startingPlayer = Random.Range(1, 3);
            Hashtable roomCustomProps = new() { { Constants.STARTPLAYER, startingPlayer } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomCustomProps);
            StartTurn(startingPlayer);
        }
    }

    public void TurnFinished()
    {
        StartTurn(otherPlayerNumber);
    }

    void StartTurn(int playerNumber)
    {
        object[] content = new object[] { playerNumber };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(Constants.START_TURN_CODE, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == Constants.START_TURN_CODE)
        {
            object[] data = (object[])photonEvent.CustomData;
            int turnPlayer = (int)data[0];
            squareGrid.CheckLines();
            if (turnPlayer == playerNumber)
            {
                turnText.text = "Your turn";
                myTurn = true;
            }
            else
            {
                turnText.text = "Opponents turn";
                myTurn = false;
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        turnText.text = "Other player left";
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("Lobby");
    }
}

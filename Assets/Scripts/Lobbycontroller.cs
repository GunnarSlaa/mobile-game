using System.Collections;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Lobbycontroller : MonoBehaviourPunCallbacks
{
    private const string STARTPLAYER = "s";

    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 2 };
        ExitGames.Client.Photon.Hashtable roomCustomProps = new ExitGames.Client.Photon.Hashtable();
        roomCustomProps.Add(STARTPLAYER, 0);
        roomOptions.CustomRoomProperties = roomCustomProps;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}

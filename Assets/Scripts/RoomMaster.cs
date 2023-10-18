using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomMaster : MonoBehaviourPunCallbacks
{
    public void HostRoom()
    {
        var randomID = Random.Range(1000000, 9999999);
        PhotonNetwork.CreateRoom(randomID.ToString(),new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
        Debug.Log($"Room Created{randomID}");
    }

    public void JoinRoom(TMP_InputField roomID)
    {
        PhotonNetwork.JoinRoom(roomID.text);
    }

    public override void OnJoinedRoom()
    {
        LoadingData.SceneToLoad = "WaitingRoom";
        SceneManager.LoadScene("Loading");
    }
}

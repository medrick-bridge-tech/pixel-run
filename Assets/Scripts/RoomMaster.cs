using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RoomMaster : MonoBehaviourPunCallbacks
{
    [SerializeField] private int roomCapacity = 2;

    public void HostRoom()
    {
        var randomID = Random.Range(1000000, 9999999);
        PhotonNetwork.CreateRoom(randomID.ToString(),new RoomOptions { MaxPlayers = roomCapacity}, TypedLobby.Default);
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

    public void ChangeRoomCapacity(TMP_Dropdown dropDown)
    {
        var selectedOption = dropDown.value;
        roomCapacity = Convert.ToInt32(dropDown.options[selectedOption].text);
        Debug.Log(roomCapacity);
    }
}

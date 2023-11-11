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
    [SerializeField] private int roomIDDigits;
    
    
    public void HostRoom()
    {
        string roomID = "";
        for (int i = 0; i < roomIDDigits; i++)
            roomID += Random.Range(0, 10).ToString();
        PhotonNetwork.CreateRoom(roomID,new RoomOptions { MaxPlayers = roomCapacity}, TypedLobby.Default);
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
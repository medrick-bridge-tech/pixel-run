using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingRoomHandler : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI playerCountText;
    [SerializeField] private TextMeshProUGUI roomNameText;
    
    private void Update()
    {
        DisplayHandler();
        LoadRaceRoom();
    }

    private void LoadRaceRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            SceneManager.LoadScene("Room1");
        }
    }

    private void DisplayHandler()
    {
        if (PhotonNetwork.InRoom)
        {
            DisplayRoomPlayersCount();
            DisplayRoomName();
        }
    }
    
    private void DisplayRoomPlayersCount()
    {
        playerCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}";
    }

    private void DisplayRoomName()
    {
        roomNameText.text = $"Room: {PhotonNetwork.CurrentRoom.Name}";
    }
}

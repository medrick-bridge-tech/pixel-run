using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(AudioSource))]
public class RaceWinHandler : MonoBehaviour
{
    [SerializeField] private List<string> winners;
    [SerializeField] private GameObject winUI;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameManager gameManager;
    
    public List<string> Winners => winners;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            winners.Add(FindWinnerNickName(other.GetComponent<PhotonView>().ViewID));
            Destroy(other.gameObject.GetComponent<PlayerMovement>());
            other.GetComponent<Animator>().SetFloat("Speed", 0);
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            if (PhotonNetwork.LocalPlayer == other.GetComponent<PhotonView>().Controller)
            {
                var playerCamera = other.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
                Destroy(playerCamera);
            }
        }
    }
    
    public void DisplayWin()
    {
        winUI.SetActive(true);
        for (var i = 0; i < winners.Count; i++)
        {
            rankText.text += $"{i+1}. {winners[i]}<br>";
        }
    }

    private string FindWinnerNickName(int viewID)
    {
        var players = PhotonNetwork.PlayerList;
        foreach (var player in players)
        {
            var playerActorNumber = (player.ActorNumber * 1000) + 1;
            if (playerActorNumber == viewID)
            {
                if (player.NickName != "")
                {
                    return player.NickName;
                }
                else
                {
                    return ((player.ActorNumber*1000)+1).ToString();
                }
            }
        }

        return null;
    }
}

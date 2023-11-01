using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.XR;

[RequireComponent(typeof(AudioSource))]
public class RaceWinHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> winners;
    [SerializeField] private GameObject winUI;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameManager gameManager;

    public List<GameObject> Winners => winners;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            winners.Add(other.gameObject);
            Destroy(other.gameObject.GetComponent<PlayerMovement>());
            other.GetComponent<Animator>().SetFloat("Speed", 0);
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            CheckEndRace();
        }
    }

    private void CheckEndRace()
    {
        if (winners.Count == gameManager.PlayersList.Count)
        {
            DisplayWin();
        }
    }

    private void DisplayWin()
    {
        winUI.SetActive(true);
    }
}

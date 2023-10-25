using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PhotonCountdown : MonoBehaviourPun
{
    private float _timerCountdown = 3f;
    private float _startTime;
    private PhotonView _photonView;
    [SerializeField] private TextMeshProUGUI timerDisplayer;
    public Action onRaceStart = () => { };
    
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _startTime = Time.time;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CountDown();
        }
    }

    private void CountDown()
    {
        float timePassed = Time.time - _startTime;
        // Calculate time remaining -> use Mathf.Max to ensure that time never display a number below zero
        float timeRemaining = Mathf.Max(0, _timerCountdown - timePassed);
            
        //UpdateText(timeRemaining.ToString("0"));
        _photonView.RPC("SyncTimer", RpcTarget.All, timeRemaining);
    }

    [PunRPC]
    private void SyncTimer(float timeRemaining)
    {
        UpdateText(timeRemaining.ToString("0"));
        if (timeRemaining <= 0)
        {
            onRaceStart.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateText(string text)
    {
        timerDisplayer.text = text;
    }
}
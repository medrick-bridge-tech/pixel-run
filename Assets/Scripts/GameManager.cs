using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPun, IOnEventCallback
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private PositionMapper positionMapper;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private PhotonCountdown photonCountdown;
    [SerializeField] private Canvas loseUICanvas;

    [SerializeField] private VariableJoystick moveJoyStick;
    private GameObject _player;

    private void OnEnable()
    {
        photonCountdown.onRaceStart += StartRace;
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        photonCountdown.onRaceStart -= StartRace;
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData eventData)
    {
        byte eventCode = eventData.Code;
        if (eventCode == 1)
        {
            DisplayGameOver();
        }
    }

    private void Awake()
    {
        _player = PhotonNetwork.Instantiate("Player", startPosition.position, Quaternion.identity);
        _player.GetComponent<PlayerMovement>().joystick = moveJoyStick;
        positionMapper.SetTarget(_player);
        positionMapper.UpdateGraphics(_player.GetComponent<SpriteRenderer>().sprite);
        virtualCamera.Follow = _player.transform;
    }

    private void StartRace()
    {
        _player.GetComponent<PlayerMovement>().Unfreeze();
    }
    
    private void DisplayGameOver()
    {
        loseUICanvas.gameObject.SetActive(true);
    }
}

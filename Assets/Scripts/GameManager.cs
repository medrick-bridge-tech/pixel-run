using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPun
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private PositionMapper positionMapper;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private PhotonCountdown photonCountdown;

    private GameObject _player;

    private void OnEnable()
    {
        photonCountdown.onRaceStart += StartRace;
    }

    private void OnDisable()
    {
        photonCountdown.onRaceStart -= StartRace;
    }

    private void Awake()
    {
        _player = PhotonNetwork.Instantiate("Player", startPosition.position, Quaternion.identity);
        positionMapper.SetTarget(_player);
        positionMapper.UpdateGraphics(_player.GetComponent<SpriteRenderer>().sprite);
        virtualCamera.Follow = _player.transform;
    }

    private void StartRace()
    {
        _player.GetComponent<PlayerMovement>().Unfreeze();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPun
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private PositionMapper positionMapper;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private PhotonCountdown photonCountdown;
    [SerializeField] private Canvas loseUICanvas;
    [SerializeField] private Canvas winUICanvas;
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private RaceWinHandler winHandler;
    
    private GameObject _player;
    private PhotonView _photonView;
    private bool _isGameStart;
    
    
    [SerializeField] List<PhotonView> _playersList = new List<PhotonView>();
    public List<PhotonView> PlayersList => _playersList;

    private void OnEnable()
    {
        photonCountdown.onRaceStart += StartRace;
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Awake()
    {
        _player = PhotonNetwork.Instantiate("Player", startPosition.position, Quaternion.identity);
        _player.GetComponent<PlayerMovement>().joystick = variableJoystick;
        _player.GetComponent<PlayerLose>().onLoseGame += RemovePlayer;
        positionMapper.SetTarget(_player);
        positionMapper.UpdateGraphics(_player.GetComponent<SpriteRenderer>().sprite);
        virtualCamera.Follow = _player.transform;
    }

    private void Start()
    {
        _isGameStart = false;
    }

    private void Update()
    {
        if (_isGameStart)
        {
            HandleRace();
        }
    }
    
    private void StartRace()
    {
        _player.GetComponent<PlayerMovement>().Unfreeze();
        foreach (var player in PhotonNetwork.PlayerList)
        {
            var viewID = (player.ActorNumber * 1000) + 1;
            _playersList.Add(PhotonNetwork.GetPhotonView(viewID));
        }
        
        _isGameStart = true;
    }

    private void RemovePlayer(PhotonView playerView)
    {
        _playersList.Remove(playerView);
        virtualCamera.GetComponent<CameraHandler>().FindNewTarget();
    }

    private void HandleRace()
    {
        if (GetPlayersCount() == 0)
        {
            DisplayLose();
        }
        else if (GetPlayersCount() == winHandler.Winners.Count)
        {
            DisplayWin();
        }
    }
    
    private void DisplayLose()
    {
        loseUICanvas.gameObject.SetActive(true);
    }

    private void DisplayWin()
    {
        winUICanvas.gameObject.SetActive(true);
    }

    private int GetPlayersCount()
    {
        int index = 0;
        foreach (var player in _playersList)
        {
            if (player != null)
            {
                index++;
            }
        }

        return index;
    }
}

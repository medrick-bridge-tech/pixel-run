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
    [SerializeField] private PhotonCountdown photonCountdown;
    [SerializeField] private Collider2D gameRoomConfiner2D;
    [SerializeField] private Canvas loseUICanvas;
    [SerializeField] private Canvas winUICanvas;
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private JumpButton jumpButton;
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
        _player.GetComponent<PlayerMovement>().jumpButton = jumpButton;
        _player.GetComponent<PlayerLose>().onLoseGame += RemovePlayer;

        var playerCamera = _player.GetComponentInChildren<CinemachineVirtualCamera>();
        playerCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = gameRoomConfiner2D;
        
        positionMapper.SetTarget(_player);
        positionMapper.UpdateGraphics(_player.GetComponent<SpriteRenderer>().sprite);
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
        _isGameStart = false;
    }

    private void DisplayWin()
    {
        winHandler.DisplayWin();
        _isGameStart = false;
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

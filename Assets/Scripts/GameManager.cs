using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPun
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private PositionMapper positionMapper;
    
    private void Awake()
    {
        var player = PhotonNetwork.Instantiate("Player", startPosition.position, Quaternion.identity);
        positionMapper.SetTarget(player);
        positionMapper.UpdateGraphics(player.GetComponent<SpriteRenderer>().sprite);
    }
}

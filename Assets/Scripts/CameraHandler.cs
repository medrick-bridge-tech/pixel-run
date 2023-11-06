using System;
using Cinemachine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CameraHandler : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        CheckTargetExist();
    }

    private void CheckTargetExist()
    {
        if (_virtualCamera.Follow == null)
        {
            FindNewTarget();
        }
    }

    public void FindNewTarget()
    {
        var players = PhotonNetwork.PlayerListOthers;
        Debug.Log("Try to find new target");
        if (players.Length != 0)
        {
            var player = players[Random.Range(0, players.Length)];
            var viewID = player.ActorNumber;
            viewID = (viewID * 1000) + 1;
            PhotonView photonView = PhotonNetwork.GetPhotonView(viewID);

            if (photonView != null)
            {
                _virtualCamera.Follow = photonView.transform;
            }
        }
    }
}
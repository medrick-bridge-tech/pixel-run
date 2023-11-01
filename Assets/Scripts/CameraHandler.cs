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

    public void FindNewTarget()
    {
        var player = PhotonNetwork.LocalPlayer.GetNext();
        if (player != null)
        {
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
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

    public const Byte OnLoseAllPlayers = 1;

    private void OnEnable()
    {
        _virtualCamera.Follow.GetComponent<PlayerLose>().onLoseGame += FindNewTarget;
    }
    
    private void OnDisable()
    {
        _virtualCamera.Follow.GetComponent<PlayerLose>().onLoseGame -= FindNewTarget;
    }
    
    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void FindNewTarget()
    {
        var player = PhotonNetwork.LocalPlayer.GetNext();
        if (player!=null)
        {
            var viewID = player.ActorNumber;
            viewID = (viewID * 1000) + 1;
            Debug.Log(viewID);
            PhotonView photonView = PhotonNetwork.GetPhotonView(viewID);

            if (photonView != null)
            {
                _virtualCamera.Follow = photonView.transform;
            }
            else
            {
                SendGameOverEvent();
            }
        }
        else
        {
            SendGameOverEvent();
        }
    }

    private void SendGameOverEvent()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(OnLoseAllPlayers, null, raiseEventOptions, SendOptions.SendReliable);
    }
}
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] scriptsToIgnore;
    
    private PhotonView _photonView;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if(!_photonView.IsMine)
        {
            foreach (var script in scriptsToIgnore)
            {
                script.enabled = false;
            }
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
    }
}

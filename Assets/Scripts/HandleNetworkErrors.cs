using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleNetworkErrors : MonoBehaviourPunCallbacks
{
    [SerializeField] private float timeoutTime;
    [SerializeField] private GameObject errorUICanvas;
    
    private PhotonView _photonView;

    private void Awake()
    {
        if (FindObjectsOfType<HandleNetworkErrors>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);    
        }
    }

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        DisplayErrorUI(message);
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        DisplayErrorUI(cause.ToString());
        
        StartCoroutine(AttemptReconnect());
        
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        DisplayErrorUI(message);
    }

    private IEnumerator AttemptReconnect()
    {
        while (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
            yield return null;
        }
        
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.ReconnectAndRejoin();
            }
            else
            {
                LoadingData.SceneToLoad = "Menu";
                SceneManager.LoadScene("Loading");       
            }
        }
    }

    private void DisplayErrorUI(string error)
    {
        var errorUI = Instantiate(errorUICanvas, transform.position, Quaternion.identity);
        var errorTextMesh = errorUI.GetComponentInChildren<TextMeshProUGUI>();
        if (errorTextMesh != null)
        {
            errorTextMesh.text = error;
        }
        Destroy(errorUI,5);
    }

    public override void OnLeftRoom()
    {
        LoadingData.SceneToLoad = "Menu";
        SceneManager.LoadScene("Loading");
    }
}
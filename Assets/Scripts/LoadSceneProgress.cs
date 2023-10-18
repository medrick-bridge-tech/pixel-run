using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneProgress : MonoBehaviourPunCallbacks
{
    [SerializeField] private Image loadingProgressFillImage;
    
    private AsyncOperation _loadOperation;

    private void Start()
    {
        if(PhotonNetwork.IsConnectedAndReady)
            StartLoadingProgress();
    }

    public override void OnConnectedToMaster()
    {
        StartLoadingProgress();
    }

    private void Update()
    {
        DisplayProgress();
    }

    private void StartLoadingProgress()
    {
        _loadOperation = SceneManager.LoadSceneAsync(LoadingData.SceneToLoad);
    }
    
    private void DisplayProgress()
    {
        if(_loadOperation != null)
            loadingProgressFillImage.fillAmount = Mathf.Clamp01(_loadOperation.progress / 0.9f);
    }
}

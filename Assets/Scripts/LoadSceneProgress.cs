using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneProgress : MonoBehaviour
{
    [SerializeField] private Image loadingProgressFillImage;
    
    private AsyncOperation _loadOperation; 
    
    private void Start()
    {
        _loadOperation = SceneManager.LoadSceneAsync(LoadingData.SceneToLoad);
    }

    private void Update()
    {
        DisplayProgress();
    }

    private void DisplayProgress()
    {
        loadingProgressFillImage.fillAmount = Mathf.Clamp01(_loadOperation.progress / 0.9f);
    }
}

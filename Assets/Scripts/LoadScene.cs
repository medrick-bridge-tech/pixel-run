using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private List<Object> sceneList;
    
    public void LoadByName(string sceneName)
    {
        LoadingData.SceneToLoad = sceneName;
        SceneManager.LoadScene("Loading");
    }

    public void LoadByObject(Object sceneObject)
    {
        LoadingData.SceneToLoad = sceneObject.name;
        SceneManager.LoadScene("Loading");
    }
    
    public void LoadRandomScene()
    {
        if (sceneList.Count > 0)
        {
            var randomNumber = Random.Range(0, sceneList.Count);
            LoadingData.SceneToLoad = sceneList[randomNumber].name;
            SceneManager.LoadScene("Loading");
        }
    }
}

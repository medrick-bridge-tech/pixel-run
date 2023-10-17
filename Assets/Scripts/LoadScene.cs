using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
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
}

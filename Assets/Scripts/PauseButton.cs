using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void DisplayPauseMenu(GameObject pauseMenu)
    {
        pauseMenu.SetActive(true);
    }
}

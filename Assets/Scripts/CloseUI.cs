using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUI : MonoBehaviour
{
    public void CloseUIElement(GameObject uiElement)
    {
        uiElement.SetActive(false);
    }
}

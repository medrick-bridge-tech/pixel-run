using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StartSkinCardIndexChanger : MonoBehaviour
{
    public Action onIncreaseFrame = () => { };
    public Action onDecreaseFrame = () => {};

    public void IncreaseFrame()
    {
        onIncreaseFrame.Invoke();
    }

    public void DecreaseFrame()
    {
        onDecreaseFrame.Invoke();
    }
}

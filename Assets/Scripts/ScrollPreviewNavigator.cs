using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ScrollPreviewNavigator : MonoBehaviour
{
    public Action onForwardScroll = () => { };
    public Action onBackwardScroll = () => {};

    public void IncreaseFrame()
    {
        onForwardScroll.Invoke();
    }

    public void DecreaseFrame()
    {
        onBackwardScroll.Invoke();
    }
}

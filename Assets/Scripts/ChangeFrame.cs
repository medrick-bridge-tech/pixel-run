using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ChangeFrame : MonoBehaviour
{
    public Action onIncreaseFrame;
    public Action onDecreaseFrame;

    public void IncreaseFrame()
    {
        onIncreaseFrame?.Invoke();
        Debug.Log("Increase");
    }

    public void DecreaseFrame()
    {
        onDecreaseFrame?.Invoke();
        Debug.Log("Decrease");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Timer : MonoBehaviour
{
    [SerializeField] private float counterTime;
    [SerializeField] private TextMeshProUGUI timerText;

    private float _currentTime;
    
    private void Start()
    {
        _currentTime = counterTime;
    }

    private void Update()
    {
        if (_currentTime >= 0)
        {
            DecreaseTime();
            DisplayTime();
        }
        else
        {
            Destroy(timerText);
        }
    }

    private void DecreaseTime()
    {
        _currentTime -=  Time.deltaTime;
    }

    private void DisplayTime()
    {
        timerText.text = _currentTime.ToString("0");
    }

    public float ReturnTime()
    {
        return _currentTime;
    }
}

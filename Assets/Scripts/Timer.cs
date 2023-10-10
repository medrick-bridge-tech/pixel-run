using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Timer : MonoBehaviourSingleton<Timer>
{
    [SerializeField] private float counterTime;
    [SerializeField] private TextMeshProUGUI timerText;

    public Action onFinishCountDown;
    
    private float _currentTime;
    private void Start()
    {
        _currentTime = counterTime;
    }

    private void Update()
    {
        if (_currentTime > 0)
        {
            DecreaseTime();
            DisplayTime();
        }
        else
        {
            onFinishCountDown?.Invoke();
            Destroy(timerText);
        }
    }

    private void DecreaseTime()
    {
        _currentTime -= 1 * Time.deltaTime;
    }

    private void DisplayTime()
    {
        timerText.text = _currentTime.ToString("0");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class UIProgress : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject target;
    
    private float _distance;
    private void Awake()
    {
        _distance = (end.position.x - start.position.x);
        Debug.Log(_distance);
    }

    private void Update()
    {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        var position = Mathf.InverseLerp(start.position.x, end.position.x, target.transform.position.x);
        slider.value = Mathf.InverseLerp(slider.minValue, slider.maxValue, position);
    }
}

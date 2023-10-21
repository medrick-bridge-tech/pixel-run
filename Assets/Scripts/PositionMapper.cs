using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PositionMapper : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject target;

    private void Update()
    {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        if (target)
        {
            var position = Mathf.InverseLerp(start.position.x, end.position.x, target.transform.position.x);
            slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, position);
        }
    }

    public void SetTarget(GameObject targetObject)
    {
        target = targetObject;
    }
}

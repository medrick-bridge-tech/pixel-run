using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isJumpButtonTouched;

    public void OnPointerDown (PointerEventData eventData)
    {
        isJumpButtonTouched = true;
    }

    public void OnPointerUp (PointerEventData eventData)
    {
        isJumpButtonTouched = false;
    }
}
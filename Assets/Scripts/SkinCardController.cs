using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinCardController : MonoBehaviour
{
    [SerializeField] private Image cardSkin;
    [SerializeField] private TextMeshProUGUI cardText;

    public void SetCard(Sprite skin, string name)
    {
        cardSkin.sprite = skin;
        cardText.text = name;
    }

    public void SelectSkin()
    {
        PlayerPrefs.SetString("skin",cardSkin.sprite.name);
    }
}

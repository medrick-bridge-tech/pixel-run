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

    public Action<Sprite> onSkinChange;

    private GameObject _skinPrefab;
    
    public void SetCard(Sprite skin, string name)
    {
        cardSkin.sprite = skin;
        cardText.text = name;
    }

    public void SelectSkin()
    {
        PlayerPrefs.SetString(PlayerPrefKeys.SKIN,cardText.text);
        onSkinChange.Invoke(cardSkin.sprite);
    }
}

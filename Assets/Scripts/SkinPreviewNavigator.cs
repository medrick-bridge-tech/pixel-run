using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class SkinPreviewNavigator : MonoBehaviour
{
    [SerializeField] private List<SkinCardController> skinCards;
    [SerializeField] private Image skinImagePlace;
    [SerializeField] private SkinCardDisplayer skinCardDisplayer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("skin"))
            skinImagePlace.sprite = Resources.Load<GameObject>(PlayerPrefs.GetString("skin")).GetComponent<SpriteRenderer>().sprite;
        else
            skinImagePlace.sprite = Resources.Load<GameObject>("Mario_Prefab").GetComponent<SpriteRenderer>().sprite;
    }
    
    private void OnEnable()
    {
        skinCardDisplayer.onRegisterSkinCard += RegisterCard;
    }

    private void UpdateCurrentSkinCardImage(Sprite currentSkin)
    {
        skinImagePlace.sprite = currentSkin;
    }

    private void RegisterCard(SkinCardController skinCard)
    {
        skinCards.Add(skinCard);
        skinCard.onSkinChange += UpdateCurrentSkinCardImage;
    }
}

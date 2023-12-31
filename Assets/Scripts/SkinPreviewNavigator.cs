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
        var skin = PlayerPrefs.GetString(PlayerPrefKeys.SKIN,"Mario");
        var path = $"{skin}/{skin}";
        skinImagePlace.sprite = Resources.Load<Sprite>(path);
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

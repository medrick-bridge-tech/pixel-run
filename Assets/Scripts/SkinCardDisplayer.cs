using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkinCardDisplayer : MonoBehaviour
{
    [SerializeField] private Skins skins;
    [SerializeField] private int skinCountPerRow;
    [SerializeField] private GameObject cardsPrefab;
    [SerializeField] private HorizontalLayoutGroup cardsGroup;
    [Space]
    [SerializeField] private ScrollPreviewNavigator forwardScroll;
    [SerializeField] private ScrollPreviewNavigator backwardScroll;

    public Action<SkinCardController> onRegisterSkinCard;
    public int startCardIndex;

    private void Start()
    {
        startCardIndex = 0;
        SpawnCards();
    }

    private void OnEnable()
    {
        forwardScroll.onForwardScroll += ForwardScroll;
        backwardScroll.onBackwardScroll += BackwardScroll;
    }
    
    private void OnDisable()
    {
        forwardScroll.onForwardScroll -= ForwardScroll;
        backwardScroll.onBackwardScroll -= BackwardScroll;
    }

    private void SpawnCards()
    {
        RemoveCards();
        for (var i = startCardIndex; i < startCardIndex + skinCountPerRow; i++)
        {
            if (i < skins.SkinCards.Count)
                CreateCard(skins.SkinCards[i]);
        }
    }

    private void CreateCard(SkinCard skinCard)
    {
        var card = Instantiate(cardsPrefab, Vector2.zero, Quaternion.identity);
        card.transform.SetParent(cardsGroup.transform);
        SkinCardController skinCardController = card.GetComponent<SkinCardController>();
        skinCardController.SetCard(skinCard.skinSprite,skinCard.name);
        onRegisterSkinCard.Invoke(skinCardController);
    }

    private void RemoveCards()
    {
        var cards = cardsGroup.GetComponentsInChildren<SkinCardController>();
        
        foreach (var card in cards)
            Destroy(card.gameObject);
    }

    private void ForwardScroll()
    {
        RemoveCards();
        if (startCardIndex < skins.SkinCards.Count - skinCountPerRow)
            startCardIndex += skinCountPerRow;
        SpawnCards();
    }
    
    private void BackwardScroll()
    {
        RemoveCards();
        if (startCardIndex - skinCountPerRow >= 0)
            startCardIndex -= skinCountPerRow;
        SpawnCards();
    }
}

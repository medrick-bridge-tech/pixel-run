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
    [SerializeField] private StartSkinCardIndexChanger increaseFrame;
    [SerializeField] private StartSkinCardIndexChanger decreaseFrame;
    
    public int startCardIndex;

    private void Start()
    {
        startCardIndex = 0;
        SpawnCards();
    }

    private void OnEnable()
    {
        increaseFrame.onIncreaseFrame += IncreaseFrame;
        decreaseFrame.onDecreaseFrame += DecreaseFrame;
    }
    
    private void OnDisable()
    {
        increaseFrame.onIncreaseFrame -= IncreaseFrame;
        decreaseFrame.onDecreaseFrame -= DecreaseFrame;
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
        card.GetComponent<SkinCardController>().SetCard(skinCard.skinSprite,skinCard.name);
    }

    private void RemoveCards()
    {
        var cards = cardsGroup.GetComponentsInChildren<SkinCardController>();
        
        foreach (var card in cards)
            Destroy(card.gameObject);
    }

    private void IncreaseFrame()
    {
        RemoveCards();
        if (startCardIndex < skins.SkinCards.Count - skinCountPerRow)
            startCardIndex += skinCountPerRow;
        SpawnCards();
    }
    
    private void DecreaseFrame()
    {
        RemoveCards();
        if (startCardIndex - skinCountPerRow >= 0)
            startCardIndex -= skinCountPerRow;
        SpawnCards();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SkinCardDisplayer : MonoBehaviour
{
    [SerializeField] private Skins skins;
    [SerializeField] private int skinCountPerRow;
    [SerializeField] private GameObject cardsPrefab;
    [SerializeField] private HorizontalLayoutGroup cardsGroup;
    [Space] 
    [SerializeField] private ChangeFrame increaseFrame;
    [SerializeField] private ChangeFrame decreaseFrame;
    
    public int frame;

    public int SkinCountPerRow => skinCountPerRow;

    private void Start()
    {
        frame = 0;
        SpawnCards();
        Debug.Log(PlayerPrefs.GetString("skin"));
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
        for (var i = frame; i < frame + skinCountPerRow; i++)
        {
            if(i<skins.SkinCards.Count)
                CreateCard(skins.SkinCards[i]);
        }
    }

    private void CreateCard(SkinCard skinCard)
    {
        var card = Instantiate(cardsPrefab, Vector2.zero, Quaternion.identity);
        card.transform.SetParent(cardsGroup.transform);
        
        card.GetComponent<SkinCardController>().SetCard(skinCard.skinSprite,skinCard.name);
        
        if(skinCard.skinSprite.name == PlayerPrefs.GetString("skin"))
            card.GetComponent<Image>().color = Color.red;
    }

    private void RemoveCards()
    {
        var cards = cardsGroup.GetComponentsInChildren<SkinCardController>();
        
        foreach (var card in cards)
        {
            Destroy(card.gameObject);
        }
    }

    private void IncreaseFrame()
    {
        RemoveCards();
        if(frame < skins.SkinCards.Count - skinCountPerRow)
            frame += SkinCountPerRow;
        SpawnCards();
    }
    
    private void DecreaseFrame()
    {
        RemoveCards();
        if(frame - SkinCountPerRow >= 0)
            frame -= SkinCountPerRow;
        SpawnCards();
    }
}

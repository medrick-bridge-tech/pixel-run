using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkinCard{
    public string name;
    public int price;
    public Sprite skinIcon;
    public GameObject skinPrefab;
}

[CreateAssetMenu(fileName = "Skin", menuName = "Skin")]
public class Skins : ScriptableObject
{
    [SerializeField] private List<SkinCard> skinCards = new List<SkinCard>();

    public List<SkinCard> SkinCards => skinCards;
}

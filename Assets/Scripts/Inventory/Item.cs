using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventoru/Item")]
public class Item : ScriptableObject
{
    [Header("Характиристики предмета")]
    public int ID;
    public string Name = " ";
    public string Discriptin = "";
    public string DiscriptinLore = "";
    public Sprite icon = null;
    public ItemType itemType;

    [Header("Вторичные свойства")]
    public float powerHeal;
}
public enum ItemType
{
    Healing,
    Magazine,
    Book   
}

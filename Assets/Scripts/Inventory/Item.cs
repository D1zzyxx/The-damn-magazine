using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//скрипт, позвол€ющий создавать собственные оъекты юнити, со своими характиристиками и тд
[CreateAssetMenu(fileName = "New Item", menuName = "Inventoru/Item")]
public class Item : ScriptableObject
{
    [Header("Ѕазовые характиристики")]
    public string Name = " ";
    public string Discriptin = "";
    public Sprite icon = null;
}

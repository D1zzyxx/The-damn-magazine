using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������, ����������� ��������� ����������� ������ �����, �� ������ ���������������� � ��
[CreateAssetMenu(fileName = "New Item", menuName = "Inventoru/Item")]
public class Item : ScriptableObject
{
    [Header("������� ��������������")]
    public int ID;
    public string Name = " ";
    public string Discriptin = "";
    public string DiscriptinLore = "";
    public Sprite icon = null;

    public bool isHealing;
    public float healingPower;
}

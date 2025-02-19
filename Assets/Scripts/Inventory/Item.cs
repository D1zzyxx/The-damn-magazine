using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������, ����������� ��������� ����������� ������ �����, �� ������ ���������������� � ��
[CreateAssetMenu(fileName = "New Item", menuName = "Inventoru/Item")]
public class Item : ScriptableObject
{
    [Header("������� ��������������")]
    public string Name = " ";
    public string Discriptin = "";
    public Sprite icon = null;
}

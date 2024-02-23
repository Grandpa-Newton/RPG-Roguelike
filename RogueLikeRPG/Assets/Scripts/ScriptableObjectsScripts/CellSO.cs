using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cell_", menuName = "CellSO")]
public class CellSO : ScriptableObject
{
    public string SceneToLoad;

    public Color CellColor;

    public Sprite OriginalSprite; // ������, ������� ��������� ��� ��������

    public Sprite NextSprite; // ������, ������� ������������ ����� ����������� ������� ������

    // ��� ����� ����� ������� � �.�., ��������� � ��������
}

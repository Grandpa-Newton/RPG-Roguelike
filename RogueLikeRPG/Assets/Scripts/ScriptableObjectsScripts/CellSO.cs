using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cell_", menuName = "CellSO")]
public class CellSO : ScriptableObject
{
    public string SceneToLoad;

    public Color CellColor;

    public Sprite OriginalSprite; // спрайт, который применяет при создании

    public Sprite NextSprite; // спрайт, который используется после прохождения данного уровня

    // Тут потом будут спрайты и т.п., связанное с визуалом
}

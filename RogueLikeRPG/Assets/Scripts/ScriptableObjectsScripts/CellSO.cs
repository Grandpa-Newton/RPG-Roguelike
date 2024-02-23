using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cell_", menuName = "CellSO")]
public class CellSO : ScriptableObject
{
    public string SceneToLoad;

    public Color CellColor;
    // Тут потом будут спрайты и т.п., связанное с визуалом
}

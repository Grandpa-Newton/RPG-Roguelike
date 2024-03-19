using UnityEngine;

namespace App.Scripts.MapScene.Cells
{
    [CreateAssetMenu(fileName = "Cell_", menuName = "CellSO")]
    public class CellSO : ScriptableObject
    {
        public string SceneToLoad;

        public Color CellColor;

        public Sprite OriginalSprite; // спрайт, который применяет при создании

        public Sprite NextSprite; // спрайт, который используется после прохождения данного уровня
    }
}

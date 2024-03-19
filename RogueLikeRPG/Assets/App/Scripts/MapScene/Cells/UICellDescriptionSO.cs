using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.MapScene.Cells
{
    [CreateAssetMenu(fileName = "_UICellDescription", menuName = "UI/CellDescription")]
    public class UICellDescriptionSO : ScriptableObject
    {
        public string levelName;
        public string levelDifficulty;
        public Color difficultyColor;
        public List<Sprite> enemyListOnLevel;
        public Sprite illustrationImage;
        public string levelDescription;
    
    }
}

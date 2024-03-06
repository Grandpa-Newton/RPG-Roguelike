using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "_UICellDescription", menuName = "UI/CellDescription")]
public class UICellDescriptionSO : ScriptableObject
{
    public string levelName;
    public string levelDifficulty;
    public Sprite levelIcon;
    public Color difficultyColor;
    public List<Sprite> enemyListOnLevel;
    public Sprite illustrationImage;
    public string levelDescription;
    
}

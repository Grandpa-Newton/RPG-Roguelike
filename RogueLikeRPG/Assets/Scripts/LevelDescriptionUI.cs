using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelDescriptionUI : MonoBehaviour
{
    [SerializeField] private UICellDescriptionSO uiCellDescriptionSo;

    [Header("UI Parameters")] 
    [SerializeField] private TMP_Text levelName;
    [SerializeField] private TMP_Text levelDifficulty;
    [SerializeField] private Image difficultyColor;
    [SerializeField] private List<Image> enemyListOnLevel;
    [SerializeField] private Image illustrationImage;
    [SerializeField] private TMP_Text levelDescription;
        
    
    void Start()
    {
        levelName.text = uiCellDescriptionSo.levelName;
        levelDifficulty.text = uiCellDescriptionSo.levelDifficulty;
        difficultyColor.color = uiCellDescriptionSo.difficultyColor;
        illustrationImage.sprite = uiCellDescriptionSo.illustrationImage;
        levelDescription.text = uiCellDescriptionSo.levelDescription;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

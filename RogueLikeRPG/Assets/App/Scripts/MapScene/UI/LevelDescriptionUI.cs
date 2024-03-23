using System.Collections.Generic;
using App.Scripts.MapScene.Cells;
using App.Scripts.MixedScenes.Player.Control;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.MapScene.UI
{
    public class LevelDescriptionUI : MonoBehaviour
    {
        private UICellDescriptionSO _uiCellDescriptionSo;

        [SerializeField] private FadeInCellDescription fadeInCellDescription;

        [Header("UI Parameters")] 
        [SerializeField] private TMP_Text levelName;
        [SerializeField] private TMP_Text levelDifficulty;
        [SerializeField] private Image difficultyColor;
        [SerializeField] private List<Image> enemyListOnLevel;
        [SerializeField] private Image illustrationImage;
        [SerializeField] private TMP_Text levelDescription;
        
    
        void Start()
        {
            /*levelName.text = uiCellDescriptionSo.levelName;
            levelDifficulty.text = uiCellDescriptionSo.levelDifficulty;
            difficultyColor.color = uiCellDescriptionSo.difficultyColor;
            illustrationImage.sprite = uiCellDescriptionSo.illustrationImage;
            levelDescription.text = uiCellDescriptionSo.levelDescription;*/

            MapPlayerController.Instance.OnChangingSelectingCell += Instance_OnChangingSelectingCell;
            MapPlayerController.Instance.OnDeselectCells += Instance_OnDeselectCells;
        }

        private void Instance_OnDeselectCells()
        {
            fadeInCellDescription.FadeOutM();
            MapPlayerController.Instance.OnChangingSelectingCell -= Instance_OnChangingSelectingCell;
        }

        private void Instance_OnChangingSelectingCell()
        {
            if(MapPlayerController.Instance.SelectingCell == null)
            {
                fadeInCellDescription.FadeOutM();
                return;
            }

            fadeInCellDescription.FadeInM();

            UICellDescriptionSO uiCellDescriptionSo = MapPlayerController.Instance.SelectingCell.GetComponent<BaseCell>().CellData.CellDescriptionData;

            gameObject.SetActive(true);

            levelName.text = uiCellDescriptionSo.levelName;
            levelDifficulty.text = uiCellDescriptionSo.levelDifficulty;
            difficultyColor.color = uiCellDescriptionSo.difficultyColor;
            illustrationImage.sprite = uiCellDescriptionSo.illustrationImage;
            levelDescription.text = uiCellDescriptionSo.levelDescription;
        }
    }
}

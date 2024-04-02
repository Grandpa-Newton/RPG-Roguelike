using System;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Player.EditableValues;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace App.Scripts.MixedScenes.Inventory.UI
{
    public class UISlider : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private const int IncreaseHealthBarWidthAtTime = 50;
        [SerializeField] private RectTransform maxHealthBar;
        [SerializeField] private RectTransform currentHealthBar;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private RectTransform healthTextTransform;
        [SerializeField] private Image sliderImage;

        [FormerlySerializedAs("floatValue")] [SerializeField] private CharacteristicValueSO characteristicValue;
        
        private void Start()
        {
            _playerHealth = new PlayerHealth(/*characteristicValue*/);
            _playerHealth.OnPlayerIncreaseHealth += OnPlayerHealthIncreaseUI;
            _playerHealth.OnPlayerIncreaseMaxHealth += UpdateHealthBarSize;
            SetValue();
        }

        private void OnPlayerHealthIncreaseUI()
        {
            healthText.text = $" {Mathf.RoundToInt(characteristicValue.CurrentValue)} / {Mathf.RoundToInt(characteristicValue.MaxValue)}";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                //floatValue.CurrentValue += 0.1f;
                characteristicValue.MaxValue += 10;
                characteristicValue.CurrentValue += 10;
                SetValue();
                UpdateHealthBarSize(IncreaseHealthBarWidthAtTime);
            }
        }

        private void SetValue()
        {
            sliderImage.fillAmount = Mathf.Clamp01(characteristicValue.CurrentValue / (float)characteristicValue.MaxValue);
            healthText.text =
                $" {Mathf.RoundToInt(characteristicValue.CurrentValue)} / {Mathf.RoundToInt(characteristicValue.MaxValue)}";

        }

        private void UpdateHealthBarSize(int widthToAdd)
        {
            Vector2 fullHealthBarSizeDelta = maxHealthBar.sizeDelta;
            Vector2 currentHealthBarBarSizeDelta = currentHealthBar.sizeDelta;

            maxHealthBar.sizeDelta = new Vector2(fullHealthBarSizeDelta.x + widthToAdd, fullHealthBarSizeDelta.y);
            healthTextTransform.sizeDelta =
                new Vector2(fullHealthBarSizeDelta.x + widthToAdd, fullHealthBarSizeDelta.y);
            currentHealthBar.sizeDelta =
                new Vector2(currentHealthBarBarSizeDelta.x + widthToAdd, currentHealthBarBarSizeDelta.y);
        }

        private void OnEnable()
        {
            characteristicValue.OnValueChange += SetValue;
        }

        private void OnDisable()
        {
            characteristicValue.OnValueChange -= SetValue;
        }
    }
}
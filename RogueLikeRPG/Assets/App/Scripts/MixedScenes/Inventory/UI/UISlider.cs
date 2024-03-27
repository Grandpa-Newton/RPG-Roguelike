using System;
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

        [SerializeField] private FloatValueSO floatValue;
        
        private void Start()
        {
            _playerHealth = new PlayerHealth(floatValue);
            _playerHealth.OnPlayerIncreaseMaxHealth += UpdateHealthBarSize;
            SetValue();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                //floatValue.CurrentValue += 0.1f;
                floatValue.MaxValue += 0.1f;
                floatValue.CurrentValue += 0.1f;
                SetValue();
                UpdateHealthBarSize(IncreaseHealthBarWidthAtTime);
            }
        }

        private void SetValue()
        {
            sliderImage.fillAmount = Mathf.Clamp01(floatValue.CurrentValue / floatValue.MaxValue);
            healthText.text =
                $" {Mathf.RoundToInt(floatValue.CurrentValue)} / {Mathf.RoundToInt(floatValue.MaxValue)}";

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
            floatValue.OnValueChange += SetValue;
        }

        private void OnDisable()
        {
            floatValue.OnValueChange -= SetValue;
        }
    }
}
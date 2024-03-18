using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UISlider : MonoBehaviour
{
    [SerializeField]
    private Image sliderImage;

    [SerializeField]
    private FloatValueSO floatValue;

    [SerializeField] private TMP_Text healthText;
    private void Start()
    {
        SetValue(floatValue.CurrentValue);
    }

    private void SetValue(float currentValue)
    {
        sliderImage.fillAmount = Mathf.Clamp01(currentValue);
        healthText.text = $" {Mathf.RoundToInt(floatValue.CurrentValue * 100)} / {Mathf.RoundToInt(floatValue.MaxValue * 100)}";

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

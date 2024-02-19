using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField]
    private Image sliderImage;

    [SerializeField]
    private FloatValueSO floatValue;
    private void Start()
    {
        SetValue(floatValue.Value);
    }
    
    public void SetValue(float currentValue)
    {
        sliderImage.fillAmount = Mathf.Clamp01(currentValue);
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

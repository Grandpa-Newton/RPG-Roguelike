using System.Collections;
using System.Collections.Generic;
using App.Scripts.MapScene.Map;
using UnityEngine;

public class CellIcon : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public void ChangeSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void DecreaseSpriteBrightness()
    {
        SpriteBrightnessChanger.DecreaseSpriteBrightness(_spriteRenderer);
    }

    public void IncreaseSpriteBrightness()
    {
        SpriteBrightnessChanger.IncreaseSpriteBrightness(_spriteRenderer);
    }
}

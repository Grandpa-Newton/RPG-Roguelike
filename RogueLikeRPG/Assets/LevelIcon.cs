using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIcon : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite nextSprite;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void DecreaseSpriteBrightness()
    {
        float H, S, V;
        Color.RGBToHSV(_spriteRenderer.color, out H, out S, out V);
        V = 0.38f;
        _spriteRenderer.color = Color.HSVToRGB(H, S, V);
    }

    public void IncreaseSpriteBrightness()
    {
        float H, S, V;
        Color.RGBToHSV(_spriteRenderer.color, out H, out S, out V);
        V = 1f;
        _spriteRenderer.color = Color.HSVToRGB(H, S, V);
    }

    public void ChangeSprite()
    {
        _spriteRenderer.sprite = nextSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteBrightnessChanger
{
    public static void DecreaseSpriteBrightness(SpriteRenderer spriteRenderer)
    {
        float brightness = 0.38f;
        ChangeSpriteBrightness(spriteRenderer, brightness);
    }

    public static void IncreaseSpriteBrightness(SpriteRenderer spriteRenderer)
    {
        float brightness = 1f;
        ChangeSpriteBrightness(spriteRenderer, brightness);
    }

    public static void ChangeSpriteBrightness(SpriteRenderer spriteRenderer, float brightness)
    {
        float H, S, V;
        Color.RGBToHSV(spriteRenderer.color, out H, out S, out V);
        V = brightness;
        spriteRenderer.color = Color.HSVToRGB(H, S, V);
    }
}

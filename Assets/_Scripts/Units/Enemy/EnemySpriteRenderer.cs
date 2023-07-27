using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteRenderer
{
    private readonly SpriteRenderer spriteRenderer;

    public EnemySpriteRenderer(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }

    public void SetDefaultSpriteAlpha()
    {
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 1f;
        spriteRenderer.color = spriteColor;
    }
}

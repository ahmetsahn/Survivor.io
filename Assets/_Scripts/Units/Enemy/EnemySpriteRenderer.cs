using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemySpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    Color defaultColor = new(1, 1, 1, 1);

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        SetDefaultSpriteAlpha();
    }
   
    public void SetDefaultSpriteAlpha()
    {
        spriteRenderer.color = defaultColor;
    }
}

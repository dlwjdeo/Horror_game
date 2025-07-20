using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideable : MonoBehaviour
{
    public bool IsHidden { get; private set; }

    private SpriteRenderer spriteRenderer;

    public Color hiddenColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagName.HideSpot))
        {
            IsHidden = true;
            gameObject.layer = LayerMask.NameToLayer(LayerName.PlayerHidden);
            spriteRenderer.color = hiddenColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(TagName.HideSpot))
        {
            IsHidden = false;
            gameObject.layer = LayerMask.NameToLayer(LayerName.Player);
            spriteRenderer.color = originalColor;
        }
    }
}

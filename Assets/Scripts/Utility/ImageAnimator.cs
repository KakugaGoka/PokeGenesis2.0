using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Image image;

    void Update() {
        image.sprite = spriteRenderer.sprite;
    }
}

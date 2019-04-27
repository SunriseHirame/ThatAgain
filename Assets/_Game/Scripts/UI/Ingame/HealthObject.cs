using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthObject : MonoBehaviour
{
    [SerializeField] private Sprite fullHealth;
    [SerializeField] private Sprite emptyHealth;
    internal bool isFull = true;
    private Image currentSprite;

    private void Start()
    {
        currentSprite = GetComponent<Image>();
        currentSprite.sprite = fullHealth;
    }

    public void UiLoseHealth()
    {
        isFull = false;
        currentSprite.sprite = emptyHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private bool displayOnStart;
    private Canvas currentCanvas;

    private void Start()
    {
        currentCanvas = GetComponent<Canvas>();
        currentCanvas.enabled = displayOnStart;
    }

    public void DisplayCanvas()
    {
        currentCanvas.enabled = true;
    }

    public void HideCanvas()
    {
        currentCanvas.enabled = false;
    }
    
}

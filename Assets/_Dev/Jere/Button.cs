using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent ToggleOn;
    public UnityEvent ToggleOff;

    private bool toggledOn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (toggledOn)
        {
            ToggleOff.Invoke();
            toggledOn = false;
        }
        else
        {
            ToggleOn.Invoke();
            toggledOn = true;
        }
    }
}

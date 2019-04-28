using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOnEnable : MonoBehaviour
{
    public Selectable Selected;

    private void OnEnable()
    {
        Invoke(nameof(Select), 0.2f);
    }

    private void Select()
    {
        Selected.Select ();
    }

    private void Reset()
    {
        if (!Selected)
            Selected = GetComponent<Selectable>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private bool moving;
    private bool up;
    public void Toggle()
    {
        if (!moving)
        {
            StartCoroutine(Move());
            moving = true;
        }
    }

    private IEnumerator Move()
    {
        
        for (int i = 0; i < 50; i++)
        {
            int dir = up ? 1 : -1;
            transform.Translate(Vector3.up*dir*0.2f);
            yield return null;
        }

        up = !up;
        moving = false;
    }
}

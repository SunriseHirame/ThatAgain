using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoom : MonoBehaviour
{
    [SerializeField]private Camera c;
    public Transform target;
    private float size;
    private Vector3 startPos;
    
    
    private void Start()
    {
        startPos = c.transform.position;
        size = c.orthographicSize;
    }

    public void Zoom()
    {
        StartCoroutine(ZoomTo());
    }

    private IEnumerator ZoomTo()
    {
        for (float i = 0; i < 1.0; i += Time.deltaTime/2.0f)
        {
            c.orthographicSize = Mathf.Lerp(c.orthographicSize, size / 10.0f, i);
            c.transform.position = Vector3.Lerp(c.transform.position, target.position, i);
            c.transform.position = new Vector3(c.transform.position.x,c.transform.position.y,-10);
            yield return null;
        }
        StartCoroutine(ZoomBack());
    }

    public void ResetZoom()
    {
        //asd
    }

    private IEnumerator ZoomBack()
    {
        for (float i = 0; i < 1.0; i += Time.deltaTime)
        {
            c.orthographicSize = Mathf.Lerp(c.orthographicSize, size, i);
            c.transform.position = Vector3.Lerp(c.transform.position, startPos, i);
            c.transform.position = new Vector3(c.transform.position.x,c.transform.position.y,-10);
            yield return null;
        }
    }
}

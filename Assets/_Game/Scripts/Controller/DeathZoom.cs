using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DeathZoom : MonoBehaviour
{
    private Camera mainCamera;
    public Transform target;
    private float size;
    private Vector3 startPos;
    
    
    private void Start()
    {
        mainCamera = Camera.main;
        startPos = mainCamera.transform.position;
        size = mainCamera.orthographicSize;
    }

    public void Zoom()
    {
        StartCoroutine(ZoomTo());
    }

    private IEnumerator ZoomTo()
    {
        for (float i = 0; i < 1.0; i += Time.deltaTime/2.0f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, size / 10.0f, i);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, target.position, i);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x,mainCamera.transform.position.y,-10);
            yield return null;
        }
        
    }

    public void ResetZoom()
    {
        StartCoroutine(ZoomBack());
    }

    private IEnumerator ZoomBack()
    {
        for (float i = 0; i < 1.0; i += Time.deltaTime*2.0f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, size, i);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, startPos, i);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x,mainCamera.transform.position.y,-10);
            yield return null;
        }
    }
}

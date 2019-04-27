using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRescaler : MonoBehaviour
{
    public float scaleFactor = 1f;

    public bool rescaleThis = false;
    public bool rescaleChildren = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rescaleThis)
        {
            rescaleThis = false;
            RescaleThis();
        }
        else if (rescaleChildren)
        {
            rescaleChildren = false;
            RescaleChildren();
        }
    }

    /// <summary>
    /// Rescales and resizes sprites and box colliders
    /// </summary>
    /// <param name="spriteRenderer">Target GameObject Sprite Renderer to modify. </param>
    /// <param name="scaleFactor">Scales object size down, while upscaling sprite dimensions.</param>
    static void Rescale(SpriteRenderer spriteRenderer, float scaleFactor)
    {
        //Rescale gameObject
        Vector3 originalScale = spriteRenderer.transform.localScale;
        spriteRenderer.transform.localScale = Vector3.one / scaleFactor;

        //Resize sprite
        Vector2 newSize = new Vector2(originalScale.x, originalScale.y) * scaleFactor;
        spriteRenderer.size = newSize;

        //Resize Box Collider
        spriteRenderer.gameObject.GetComponent<BoxCollider2D>().size = newSize;
    }

    /// <summary>
    /// Rescales and resizes an array of sprites and box colliders.
    /// </summary>
    /// <param name="spriteRenderer">Array of GameObject Sprite Renderers to modify. </param>
    /// <param name="scaleFactor">Scales object size down, while upscaling sprite dimensions.</param>
    static void RescaleMany(SpriteRenderer[] spriteRenderer, float scaleFactor)
    {
        foreach (SpriteRenderer sr in spriteRenderer)
        {
            Rescale(sr, scaleFactor);
        }
    }

    /// <summary>
    /// Rescale all child elements that contain a sprite renderer and box collider.
    /// </summary>
    void RescaleChildren()
    {
        //Fecth all children that have both Sprite Renderer and Box Collider
        List<SpriteRenderer> validChildren = new List<SpriteRenderer>();
        gameObject.GetComponentsInChildren<SpriteRenderer>(validChildren);
        foreach (SpriteRenderer sr in validChildren)
        {
            if (sr.GetComponent<BoxCollider2D>() == null)
            {
                validChildren.Remove(sr);
            }
        }

        //Run rescale
        RescaleMany(validChildren.ToArray(), scaleFactor);
    }

    /// <summary>
    /// Rescales this objects sprites and box collider. Assumes that both components are attached.
    /// </summary>
    void RescaleThis()
    {
        Rescale(gameObject.GetComponent<SpriteRenderer>(), scaleFactor);
    }
}

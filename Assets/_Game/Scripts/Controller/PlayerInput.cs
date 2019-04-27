using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private static readonly List<PlayerInput> inputs = new List<PlayerInput> ();
    
    private const string HorizontalBase = "Horizontal";
    private const string VerticalBase = "Vertical";
    private const string JumpBase = "Jump";

    [SerializeField]
    private bool autoIndex;
    
    [SerializeField]
    private int controlIndex = -1;

    private string horizontalAxis;
    private string verticalAxis;
    private string jump;

    private void Awake ()
    {
        if (autoIndex && controlIndex == -1)
        {
            for (var i = 0; i < inputs.Count; i++)
            {
                if (inputs[i] == null)
                {
                    SetIndex (i);
                    return;
                }
            }
            SetIndex (inputs.Count);
        }
    }

    public void SetIndex (int index)
    {
        if (controlIndex != -1)
        {
            inputs[controlIndex] = null;
        }
        
        controlIndex = index;
        
        horizontalAxis = $"{HorizontalBase}{index}";
        verticalAxis = $"{VerticalBase}{index}";
        jump = $"{JumpBase}{index}";
        
        inputs.Insert (index, this);
    }

    public Vector2 GetMovement ()
    {
        return new Vector2( 
            Input.GetAxisRaw (horizontalAxis), 
            Input.GetAxisRaw (verticalAxis));
    }

    public bool GetJump ()
    {
        return Input.GetAxisRaw (jump) > 0.2f;
    }
}

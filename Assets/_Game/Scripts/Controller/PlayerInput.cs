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

    private string horizontalAxis = HorizontalBase;
    private string verticalAxis = VerticalBase;
    private string jump = JumpBase;

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
        SetIndex (-1);
    }

    public void SetIndex (int index)
    {
        if (controlIndex != -1 && inputs.Count > 0)
        {
            inputs[controlIndex] = null;
        }
        
        controlIndex = index;
        
        horizontalAxis = index == -1 ? HorizontalBase : $"{HorizontalBase}{index}";
        verticalAxis =  index == -1 ? VerticalBase : $"{VerticalBase}{index}";
        jump =  index == -1 ? JumpBase : $"{JumpBase}{index}";
        
        if (index != -1)
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
        return Input.GetButtonDown(jump); // (jump) > 0.2f;
    }
}

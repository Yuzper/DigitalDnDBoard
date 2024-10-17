using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolState : IState
{
    public virtual void EnterState()
    {
        Debug.Log($"Entering ToolState: {this.GetType().Name}");
    }

    public virtual void ExitState()
    {
        Debug.Log($"Exiting ToolState: {this.GetType().Name}");
    }

    public abstract void UpdateState();
}

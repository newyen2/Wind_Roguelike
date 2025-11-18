using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectBase : ScriptableObject
{

    public virtual void OnBeforeExecute(Wind w)
    {
    }

    public virtual void OnAfterExecute(Wind w)
    {

    }

    public virtual void OnAfterMove(Wind w)
    {

    }

}
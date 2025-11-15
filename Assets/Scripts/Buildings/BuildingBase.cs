using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingBase : ScriptableObject
{
    public string id;
    public string displayName;
    [TextArea] public string description;

    // 將來會 override 的 Hook
    //public virtual void OnBattleStart(BattleContext ctx) {}
    //public virtual void OnTurnStart(BattleContext ctx) {}
    //public virtual void OnBeforeEvaluateHand(RunContext run, HandContext hand) {}
    //public virtual void OnModifyScore(RunContext run, HandContext hand) {}
    //public virtual void OnAfterHandScored(RunContext run, HandContext hand) {}

}

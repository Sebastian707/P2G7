using UnityEngine;

public class DebugEffect : CardEffect
{
    public string debugText = "no  debug text was added";
    public override void Use()
    {
        Debug.Log(debugText);
    }
}

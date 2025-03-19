using UnityEngine;

public class DebugEffect : CardEffect
{
    public string debugText = "no  debug text was added";
    public override void Use(CardPlayerScript cardPlayer)
    {
        Debug.Log(debugText);
    }
}

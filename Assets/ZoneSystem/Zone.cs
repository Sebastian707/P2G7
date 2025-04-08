using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewZone", menuName = "Metroidvania/Zone")]
public class Zone : ScriptableObject
{
    public string zoneName;
    public List<string> sceneNames;
}
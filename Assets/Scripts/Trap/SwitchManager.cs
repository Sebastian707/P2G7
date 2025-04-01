using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager Instance;
    private Dictionary<string, bool> switchStates = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterSwitch(string id, bool defaultState)
    {
        if (!switchStates.ContainsKey(id))
        {
            switchStates[id] = defaultState;
        }
    }

    public bool HasSwitch(string id)
    {
        return switchStates.ContainsKey(id);
    }

    public bool GetSwitchState(string id)
    {
        return switchStates.ContainsKey(id) && switchStates[id];
    }

    public void SetSwitchState(string id, bool state)
    {
        switchStates[id] = state;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public string switchID; 
    public GameObject obstacle;
    public float InteractDistance = 3f;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (string.IsNullOrEmpty(switchID))
        {
            Debug.LogError("Switch ID is missing on " + gameObject.name);
            return;
        }

        if (SwitchManager.Instance != null)
        {
            if (SwitchManager.Instance.HasSwitch(switchID))
            {
                if (SwitchManager.Instance.GetSwitchState(switchID))
                {
                    turnOn();
                }
            }
            else
            {
                SwitchManager.Instance.RegisterSwitch(switchID, false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerClose())
        {
            turnOn();
        }
    }

    public void turnOn()
    {
        if (SwitchManager.Instance != null)
        {
            SwitchManager.Instance.SetSwitchState(switchID, true);
        }
        if (obstacle != null)
        {
            obstacle.GetComponent<Obstacle>().destroy();
        }
    }

    private bool IsPlayerClose()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            return distance <= InteractDistance;
        }
        return false;
    }
}

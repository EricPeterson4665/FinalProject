using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool target1Hit;
    public bool target2Hit;
    public bool target3Hit;
    public bool target4Hit;
    public bool target5Hit;
    public bool target6Hit;
    public bool target7Hit;

    public GameObject door1;
    public GameObject door2;
    public GameObject door3;

    private bool door1open;
    private bool door2open;
    private bool door3open;
    
    public void CheckGameState()
    {
        if (!door1open)
        {
            if (target1Hit && target2Hit)
            {
                door1.SendMessage("OpenDoor");
                door1open = true;
            }
        }
        
        if (!door2open)
        {
            if (target3Hit && target4Hit && target5Hit)
            {
                door2.SendMessage("OpenDoor");
                door2open = true;
            }
        }

        if (!door3open)
        {
            if (target6Hit && target7Hit)
            {
                door3.SendMessage("OpenDoor");
                door3open = true;
            }
        }

    }

}

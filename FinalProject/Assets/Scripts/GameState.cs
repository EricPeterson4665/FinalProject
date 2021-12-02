using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool target1Hit;
    public bool target2Hit;
    public GameObject door1;
    
    public void CheckGameState()
    {
        if (target1Hit  && target2Hit)
        {
            door1.SendMessage("OpenDoor");
        }

    }

}

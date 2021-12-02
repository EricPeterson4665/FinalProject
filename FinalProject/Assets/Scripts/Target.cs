using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject indicatorLight;  //assign correct light
    public GameState gameState;

    public void TargetHit()
    {
        indicatorLight.SendMessage("ActivateLight");
        if(this.name == "Target1")
        {
            gameState.target1Hit = true;
        }
        else if(this.name == "Target2")
        {
            gameState.target2Hit = true;
        }
        
        gameState.CheckGameState();
    }
    
}

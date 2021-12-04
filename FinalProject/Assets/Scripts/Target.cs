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

        switch (this.name)
        {
            case "Target1": 
                gameState.target1Hit = true; break;
            case "Target2":
                gameState.target2Hit = true; break;
            case "Target3":
                gameState.target3Hit = true; break;
            case "Target4":
                gameState.target4Hit = true; break;
            case "Target5":
                gameState.target5Hit = true; break;
            case "Target6":
                gameState.target6Hit = true; break;
            case "Target7":
                gameState.target7Hit = true; break;
            case "Target8":
                gameState.target8Hit = true; break;
            case "Target9":
                gameState.target9Hit = true; break;
            case "Target10":
                gameState.target10Hit = true; break;
            case "Target11":
                gameState.target11Hit = true; break;

        }

        /*
        if(this.name == "Target1")
        {
            gameState.target1Hit = true;
        }
        else if(this.name == "Target2")
        {
            gameState.target2Hit = true;
        }
        */
        gameState.CheckGameState();
    }
    
}

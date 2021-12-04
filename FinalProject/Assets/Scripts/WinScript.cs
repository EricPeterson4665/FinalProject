using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    public GameObject player;
    public GameState gameState;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            gameState.EndGame(true);
        }
    }
}

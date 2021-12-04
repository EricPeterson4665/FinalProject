using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameState : MonoBehaviour
{
    public float timeLimitSeconds;
    private float timer;
    public GameObject player;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI lostText;

    public bool target1Hit;
    public bool target2Hit;
    public bool target3Hit;
    public bool target4Hit;
    public bool target5Hit;
    public bool target6Hit;
    public bool target7Hit;
    public bool target8Hit;
    public bool target9Hit;
    public bool target10Hit;
    public bool target11Hit;

    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;

    private bool door1open;
    private bool door2open;
    private bool door3open;

    private bool stopTime;

    private void Start()
    {
        timer = timeLimitSeconds;
        winText.enabled = false;
        lostText.enabled = false;
    }

    private void Update()
    {
        if (!stopTime)
        {
            timer -= Time.deltaTime;
            SetTimeText();
        }

        if(timer < 0)
        {
            EndGame(false);
        }
    }

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

        if(target8Hit && target9Hit && target10Hit && target11Hit)
        {
            door4.SendMessage("OpenDoor");
        }
    }

    private void SetTimeText()
    {
        if (timeText.isActiveAndEnabled)
        {
            TimeSpan formatedTime = TimeSpan.FromSeconds(timer);
            timeText.text = String.Format(@"{0:mm\:ss\.ff}", formatedTime);
        }
    }

    public void EndGame(bool isWinState)
    {
        player.GetComponent<PlayerController>().enabled = false;
        stopTime = true;
        if (isWinState)
        {
            winText.enabled = true;
        }
        else
        {
            lostText.enabled = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject doorway;
    
    public void OpenDoor()
    {
        StartCoroutine(DoorControl());
    }

    IEnumerator DoorControl()
    {
        yield return new WaitForSeconds(0.3f);
        doorway.GetComponent<Animator>().Play("DoorOpen");
        yield return new WaitForSeconds(0.2f);
        doorway.GetComponent<AudioSource>().Play();

    }
    
}

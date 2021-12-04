using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPopup : MonoBehaviour
{
    public float topDelay;
    public float bottomDelay;
    private float timer;
    private bool audioPlayed;
    private Vector3 originalPosition;
    private Vector3 finalPosition;


    private void Start()
    {
        originalPosition = this.transform.position;
        finalPosition = originalPosition;
        finalPosition.y += 3;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= bottomDelay  && timer < bottomDelay + 1 && this.transform.position.y < finalPosition.y)
        {
            this.transform.Translate(Vector3.up * 10 * Time.deltaTime);
        }
        if(timer > bottomDelay + 0.3f && timer < bottomDelay + 0.5f && !audioPlayed)
        {
            this.GetComponent<AudioSource>().Play();
            audioPlayed = true;
        }
        else if(timer >= bottomDelay + topDelay + 1 && this.transform.position.y > originalPosition.y)
        {
            this.transform.Translate(Vector3.down * 10 * Time.deltaTime);
        }
        else if(timer >= bottomDelay + topDelay + 1.5f)
        {
            timer = 0;
            audioPlayed = false;
        }
    }
}

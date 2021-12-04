using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSlider : MonoBehaviour
{
    public float moveTime = 10;
    public float moveMagnitude = 1;
    private float timer = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer <= moveTime / 2.0f  && timer >= 0)
        {
            this.transform.Translate(Vector3.forward * moveMagnitude * Time.deltaTime);
        }
        else if(timer > moveTime / 2.0f && timer <= moveTime)
        {
            this.transform.Translate(Vector3.back * moveMagnitude * Time.deltaTime);
        }
        else
        {
            timer = 0;
        }
    }
}

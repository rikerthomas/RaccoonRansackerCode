using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    public float waitTime = 0.0f;
    public float waitTotal = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        waitTime += Time.deltaTime;
        if(waitTime > waitTotal)
        {
            Destroy(gameObject);
            waitTime = 0.0f;
        }

    }
}

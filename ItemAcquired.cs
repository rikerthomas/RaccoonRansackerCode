using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAcquired : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            audioSource.Play();
        }
    }
}

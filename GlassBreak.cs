using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreak : MonoBehaviour
{
    public GameObject broken;
    public GameObject glass;
    private BoxCollider bcollider;
    public AudioSource audioSource;


    private void Start()
    {
        bcollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            broken.SetActive(true);
            glass.SetActive(false);
            bcollider.enabled = false;
            audioSource.Play();
        }
    }
}
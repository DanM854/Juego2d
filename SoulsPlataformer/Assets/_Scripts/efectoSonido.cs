using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class efectoSonido : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip sonido;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(sonido);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curacion : MonoBehaviour
{
    public int curacion = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Bandit player = collision.gameObject.GetComponent<Bandit>();

            player.CurarVidaa(curacion);
            Destroy(gameObject);
        }
    }
}

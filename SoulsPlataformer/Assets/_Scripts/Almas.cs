using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almas : MonoBehaviour
{
    public int valor = 500;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SumarPuntos(valor);
            }
            AudioManager.instance.PlayAlmas();
            Destroy(gameObject);
        }
    }
}

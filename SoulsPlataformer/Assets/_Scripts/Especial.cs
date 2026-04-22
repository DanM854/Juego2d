using UnityEngine;

public class Especial : MonoBehaviour
{
    private bool recogida = false;
    private bool activo = false;

    void Start()
    {
        Invoke(nameof(Activar), 0.2f);
    }

    void Activar()
    {
        activo = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activo) return;
        if (recogida) return;

        if (!collision.CompareTag("Player")) return;


        recogida = true;

        Debug.Log("Moneda especial recogida");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.NivelCompletado();
        }

        Destroy(gameObject);
    }
}
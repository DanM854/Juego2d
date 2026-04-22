using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int vida = 3;
    public float fuerzaRebote = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void RecibeDanio(int danio, Vector2 posicionJugador)
    {
        vida -= danio;

        Vector2 direccion = (rb.position - posicionJugador).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(direccion * fuerzaRebote, ForceMode2D.Impulse);

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
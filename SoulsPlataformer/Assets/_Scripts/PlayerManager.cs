
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    public int vida = 3;
    public int vidaMax = 5;

    public float fuerzaSalto = 10f;
    public float fuerzaRebote = 6f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;

    private bool enSuelo;
    private bool recibiendoDanio;
    private bool atacando;
    public bool muerto;

    private Rigidbody2D rb;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (muerto)
        {
            rb.velocity = Vector2.zero;
            return;
        }


        
        

        animator.SetBool("ensuelo", enSuelo);
        animator.SetBool("recibeDanio", recibiendoDanio);
        animator.SetBool("Atacando", atacando);
        animator.SetBool("muerto", muerto);
    }

    
    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            vida -= cantDanio;
            if (vida <= 0)
            {
                muerto = true;
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.GameOver();
                    //GameManager.Instance.RespawnJugador();
                }
            }
            if (!muerto)
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.2f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }
        }
    }

    public void Respawn(Vector3 positionCheckpoint)
    {
        muerto = false;
        recibiendoDanio = false;
        atacando = false;
        vida = vidaMax;

        transform.position = positionCheckpoint;

        rb.velocity = Vector2.zero;

        animator.SetBool("muerto", false);
        animator.SetBool("recibeDanio", false);
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;
        rb.velocity = Vector2.zero;
    }

    public void Atacando()
    {
        atacando = true;
    }

    public void DesactivaAtaque()
    {
        atacando = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }
}
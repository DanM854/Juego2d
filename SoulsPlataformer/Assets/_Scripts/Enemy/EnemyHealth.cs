using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Vida")]
    public int vida = 3;
    public float fuerzaRebote = 5f;

    [Header("Ataque")]
    public float rangoAtaque = 4f;
    public int danio = 1;
    public float tiempoEntreAtaques = 1.5f;

    private float tiempoSiguienteAtaque = 0f;

    private Transform jugador;
    private Rigidbody2D rb;
    private Animator animator;
    private EnemyMovement movimiento;

    private bool muerto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movimiento = GetComponent<EnemyMovement>();

        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (muerto || jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= rangoAtaque && Time.time >= tiempoSiguienteAtaque)
        {
            Debug.Log("INTENTANDO ATACAR");
            Atacar();
        }
    }

    void Atacar()
    {
        tiempoSiguienteAtaque = Time.time + tiempoEntreAtaques;

        movimiento.canMove = false;
        rb.velocity = Vector2.zero;

        animator.SetTrigger("Attack");

        Invoke("ActivarMovimiento", 1f); // ajusta al tiempo de tu animación
    }

    void ActivarMovimiento()
    {
        if (!muerto)
        {
            movimiento.canMove = true;
        }
    }

    // ?? LLAMADO DESDE ANIMATION EVENT
    public void HacerDanio()
    {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= rangoAtaque)
        {
            jugador.GetComponent<Bandit>()?.RecibeDanio(transform.position, danio);
        }
    }

    // ?? RECIBE DAÑO (CON HURT + RETROCESO)
    public void RecibeDanio(int danioRecibido, Vector2 posicionJugador)
    {
        if (muerto) return;

        vida -= danioRecibido;

        // Animación de daño
        animator.SetTrigger("Hurt");

        // Detener movimiento
        movimiento.canMove = false;
        rb.velocity = Vector2.zero;

        // Retroceso
        Vector2 direccion = (rb.position - posicionJugador).normalized;
        rb.AddForce(direccion * fuerzaRebote, ForceMode2D.Impulse);

        if (vida <= 0)
        {
            Morir();
        }
        else
        {
            Invoke("RecuperarMovimiento", 0.5f); // tiempo de stun
        }
    }

    void RecuperarMovimiento()
    {
        if (!muerto)
        {
            movimiento.canMove = true;
        }
    }

    void Morir()
    {
        muerto = true;

        animator.SetTrigger("Death");
        rb.velocity = Vector2.zero;

        GetComponent<Collider2D>().enabled = false;
        movimiento.canMove = false;

        Destroy(gameObject, 1.5f); // ajusta al tiempo de animación
    }
}
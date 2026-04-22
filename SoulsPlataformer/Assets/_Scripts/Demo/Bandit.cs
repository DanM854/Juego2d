using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    [SerializeField] int vida = 3; // ❤️ VIDA
    [SerializeField] float fuerzaRebote = 6f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;

    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;
    private bool recibiendoDanio = false;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    void Update()
    {
        if (m_isDead) return; 

        // Detectar suelo
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", true);
        }

        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", false);
        }

        float inputX = Input.GetAxis("Horizontal");

        // Girar personaje
        if (inputX > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (inputX < 0)
            transform.localScale = new Vector3(1, 1, 1);

        // Movimiento (bloqueado si recibe daño)
        if (!recibiendoDanio)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // Inputs
        if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && m_grounded)
        {
            Saltar();
        }
        else if (Mathf.Abs(inputX) > 0.1f)
        {
            m_animator.SetInteger("AnimState", 2); // correr
        }
        else if (m_combatIdle)
        {
            m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            m_animator.SetInteger("AnimState", 0); // idle
        }
    }

    void Saltar()
    {
        m_animator.SetTrigger("Jump");
        m_grounded = false;
        m_animator.SetBool("Grounded", false);
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        m_groundSensor.Disable(0.2f);
    }

    // 💥 COLISIÓN CON ENEMIGO
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            RecibeDanio(collision.transform.position, 1);
        }
    }


    void RecibeDanio(Vector2 posicionEnemigo, int danio)
    {
        if (recibiendoDanio || m_isDead) return;

        recibiendoDanio = true;
        vida -= danio;

        m_animator.SetTrigger("Hurt");

        // Knockback
        Vector2 direccion = (m_body2d.position - posicionEnemigo).normalized;
        m_body2d.velocity = Vector2.zero;
        m_body2d.AddForce(direccion * fuerzaRebote, ForceMode2D.Impulse);

        if (vida <= 0)
        {
            Morir();
        }
        else
        {
            Invoke("ResetDanio", 0.5f);
        }
    }

    void ResetDanio()
    {
        recibiendoDanio = false;
    }

    void Morir()
    {
        m_isDead = true;
        m_animator.SetTrigger("Death");
        m_body2d.velocity = Vector2.zero;

        // Opcional: desactivar collider
        GetComponent<Collider2D>().enabled = false;
    }
}
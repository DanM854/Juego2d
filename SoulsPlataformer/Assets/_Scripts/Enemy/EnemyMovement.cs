using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Variables iniciales par cuadrar los l[imites de movimiento de los enemigos.
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;

    // Variable que guarde el punto actual de movimiento.
    private Transform currentPoint;

    // Variable que almacena el Rigidbody del enemigo.
    private Rigidbody2D rb;

    // Variable que almacena la velocidad de movimiento.
    public float speed;

    [HideInInspector] public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            // Se llaman los métodos para el movimiento.
            Movement();
            FlipMovement();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Movement()
    {
        // Valida si el enemigo se acerca a uno de los puntos para que cambie el punto objetivo.
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }

    void FlipMovement()
    {
        // Modifica la velcidad y la rotación del enemigo si llega a uno de los puntos para que se dirija al otro.
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

}


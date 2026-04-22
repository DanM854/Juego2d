using UnityEngine;
using UnityEngine.SceneManagement;


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Checkpoint")]
    public Vector3 checkpointPosition;
    public Checkpoint checkpointActual;

    private void Awake()
    {

    }

    void Start()
    {




        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            checkpointPosition = player.transform.position;
        }
    }


    void Update()
    {

    }

    public void ReiniciarEscena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ActualizarCheckpoint(Vector3 nuevaPosition, Checkpoint nuevoCheckpoint)
    {
        checkpointPosition = nuevaPosition;
        checkpointActual = nuevoCheckpoint;
    }


    public void RespawnJugador()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(1f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().Respawn(checkpointPosition);
        }
    }


}

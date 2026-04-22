using UnityEngine;
using UnityEngine.SceneManagement;


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using TMPro;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button reiniciarButton;
    public Button menuButton;

    private bool gameOverActivo = false;

    [Header("Checkpoint")]
    public Vector3 checkpointPosition;
    public Checkpoint checkpointActual;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }



    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (reiniciarButton != null)
            reiniciarButton.onClick.AddListener(ReiniciarEscena);

        if (menuButton != null)
            menuButton.onClick.AddListener(IrAlMenu);



        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            checkpointPosition = player.transform.position;
        }

        
        Debug.Log("Panel activo al iniciar: " + gameOverPanel.activeSelf);
        
    }


    void Update()
    {
        if (gameOverActivo)
        {
            if(Input.GetKeyUp(KeyCode.R))
            {
                ReiniciarEscena();
            }

            if(Input.GetKeyUp(KeyCode.Escape))
            {
                IrAlMenu();
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("SE ACTIVÓ GAME OVER");
        if (gameOverActivo) return;

        gameOverActivo = true;

        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER\n\nR - Reiniciar\nESC - Menu";
        }
    }

    public void ReiniciarEscena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{

    public Image rellenoBarraVida;
    private Bandit bandit;
    private float vidaMaxima;
    void Start()
    {
        bandit = GameObject.Find("Player").GetComponent<Bandit>();
        vidaMaxima = bandit.vida;
    }

    void Update()
    {
        rellenoBarraVida.fillAmount = bandit.vida / vidaMaxima;
    }
}

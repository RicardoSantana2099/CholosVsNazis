using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    private Slider slider;
    private int vidasMaximas;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void InicializarBarraDeVida(int cantidadVidas)
    {
        vidasMaximas = cantidadVidas;
        CambiarVidaActual(vidasMaximas);
    }

    public void CambiarVidaActual(int cantidaVidas)
    {
        slider.value = cantidaVidas;
    }

    public void ReducirVida()
    {
        if (vidasMaximas > 0)
        {
            vidasMaximas--;
            CambiarVidaActual(vidasMaximas);
        }
    }
}


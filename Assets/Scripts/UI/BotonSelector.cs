using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BotonSelector : MonoBehaviour
{
    public string textoAEnviar;
    public GameObject receptor;

    public void EnviarArma()
    {
        receptor.GetComponent<CambioSkin>().ArmaSelect = textoAEnviar;
    }

    public void EnviarBarba()
    {
        receptor.GetComponent<CambioSkin>().BarbaSelect= textoAEnviar;
    }

    public void EnviarCuerpo()
    {
        receptor.GetComponent<CambioSkin>().CuerpoSelect = textoAEnviar;
    }

    public void EnviarEscudo()
    {
        receptor.GetComponent<CambioSkin>().EscudoSelect = textoAEnviar;
    }

    public void EnviarPelo()
    {
        receptor.GetComponent<CambioSkin>().PeloSelect = textoAEnviar;
    }
}

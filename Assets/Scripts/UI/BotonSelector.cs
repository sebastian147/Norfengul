using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// La clase BotonSelector envía diferentes selecciones (arma, barba, cuerpo, escudo, pelo) 
/// a un objeto receptor que implementa el componente CambioSkin.
/// </summary>
public class BotonSelector : MonoBehaviour
{
    // Texto que contiene la selección de skin que se enviará
    public string textoAEnviar;
    
    // El objeto que recibirá el cambio de skin
    public GameObject receptor;

    /// <summary>
    /// Envía la selección de arma al componente CambioSkin del receptor.
    /// </summary>
    public void EnviarArma()
    {
        receptor.GetComponent<CambioSkin>().ArmaSelect = textoAEnviar;
    }

    /// <summary>
    /// Envía la selección de barba al componente CambioSkin del receptor.
    /// </summary>
    public void EnviarBarba()
    {
        receptor.GetComponent<CambioSkin>().BarbaSelect = textoAEnviar;
    }

    /// <summary>
    /// Envía la selección de cuerpo al componente CambioSkin del receptor.
    /// </summary>
    public void EnviarCuerpo()
    {
        receptor.GetComponent<CambioSkin>().CuerpoSelect = textoAEnviar;
    }

    /// <summary>
    /// Envía la selección de escudo al componente CambioSkin del receptor.
    /// </summary>
    public void EnviarEscudo()
    {
        receptor.GetComponent<CambioSkin>().EscudoSelect = textoAEnviar;
    }

    /// <summary>
    /// Envía la selección de pelo al componente CambioSkin del receptor.
    /// </summary>
    public void EnviarPelo()
    {
        receptor.GetComponent<CambioSkin>().PeloSelect = textoAEnviar;
    }

    // TODO: Evaluar si conviene usar eventos en lugar de llamar al método del componente directamente
}
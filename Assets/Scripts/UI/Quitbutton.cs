using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase QuitButton gestiona la salida de la aplicación 
/// al ser activada desde la interfaz de usuario.
/// </summary>
public class QuitButton : MonoBehaviour
{
    /// <summary>
    /// Cierra la aplicación cuando se llama a este método.
    /// En el editor de Unity no realiza ninguna acción.
    /// </summary>
    public void QuitApplication()
    {
        // Cierra la aplicación
        Application.Quit();
        
        // TODO: Añadir un mensaje de confirmación antes de cerrar la aplicación.
    }
}
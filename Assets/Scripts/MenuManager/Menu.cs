using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que representa un menú, gestionando su apertura y cierre.
/// </summary>
public class Menu : MonoBehaviour
{
    // Nombre del menú
    
    // Estado del menú, si está abierto o cerrado
    private bool open;

    /// <summary>
    /// Obtiene o establece el nombre del menú.
    /// </summary> 
    [SerializeField]
    public string MenuName;

    /// <summary>
    /// Obtiene o establece el estado de apertura del menú.
    /// </summary>
    public bool Open
    {
        get { return open; }
        set { open = value; }
    }

    /// <summary>
    /// Abre el menú activando el GameObject asociado.
    /// </summary>
    public void OpenMenu()
    {
        open = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Cierra el menú desactivando el GameObject asociado.
    /// </summary>
    public void CloseMenu()
    {
        open = false;
        gameObject.SetActive(false);
    }

    // TODO: Considerar agregar animaciones al abrir/cerrar el menú para una mejor experiencia visual.
}

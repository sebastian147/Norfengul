using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que gestiona la apertura y cierre de múltiples menús.
/// </summary>
public class MenuManager : MonoBehaviour
{
    // Instancia estática del gestor de menús
    public static MenuManager Instance { get; private set; }

    // Lista de menús gestionados por el MenuManager
    [SerializeField] private Menu[] menus;

    /// <summary>
    /// Configura la instancia del gestor al despertar.
    /// </summary>
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Abre un menú específico por su nombre y cierra los otros que estén abiertos.
    /// </summary>
    /// <param name="menuName">El nombre del menú a abrir.</param>
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].MenuName == menuName)
            {
                menus[i].OpenMenu();
            }
            else if (menus[i].Open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    /// <summary>
    /// Abre un menú específico y cierra los otros que estén abiertos.
    /// </summary>
    /// <param name="menu">El menú a abrir.</param>
    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].Open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.OpenMenu();
    }

    /// <summary>
    /// Cierra un menú específico.
    /// </summary>
    /// <param name="menu">El menú a cerrar.</param>
    public void CloseMenu(Menu menu)
    {
        menu.CloseMenu();
    }
    public void CloseMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].MenuName == menuName)
            {
                CloseMenu(menus[i]);
            }
        }
    }
    // TODO: Agregar funcionalidad para detectar cuando no hay menús abiertos.
}

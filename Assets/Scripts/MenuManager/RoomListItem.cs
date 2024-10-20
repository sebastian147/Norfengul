using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

/// <summary>
/// Clase que representa un ítem en la lista de salas.
/// </summary>
public class RoomListItem : MonoBehaviour
{
    // Texto que muestra el nombre de la sala
    [SerializeField] private TMP_Text text;

    // Información de la sala asociada a este ítem
    private RoomInfo info;

    /// <summary>
    /// Configura el ítem con la información de la sala.
    /// </summary>
    /// <param name="_info">Información de la sala.</param>
    public void SetUp(RoomInfo _info)
    {
        info = _info;
        text.text = _info.Name;
    }

    /// <summary>
    /// Llama a la instancia de conexión para unirse a la sala cuando se selecciona.
    /// </summary>
    public void OnClick()
    {
        if (info != null)
        {
            ConnectToServer.Instance.JoinRoom(info);
        }
        else
        {
            Debug.LogError("RoomInfo es nulo al intentar unirse.");
        }
    }

    // TODO: Agregar una validación visual si la sala está llena o no disponible.
}

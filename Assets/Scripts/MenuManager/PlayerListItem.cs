using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

/// <summary>
/// Clase que representa un ítem en la lista de jugadores.
/// </summary>
public class PlayerListItem : MonoBehaviourPunCallbacks
{
    // Texto que muestra el nombre del jugador
    [SerializeField] private TMP_Text text;

    // Información del jugador asociado a este ítem
    private Player player;

    /// <summary>
    /// Configura el ítem con la información del jugador.
    /// </summary>
    /// <param name="_player">Información del jugador.</param>
    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
    }

    /// <summary>
    /// Callback que se llama cuando un jugador abandona la sala.
    /// </summary>
    /// <param name="otherPlayer">Jugador que abandonó la sala.</param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player != null && player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Callback que se llama cuando abandonas la sala.
    /// </summary>
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

    // TODO: Agregar funcionalidad para manejar la desconexión de jugadores inesperadamente.
}

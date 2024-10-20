using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using System.Linq;

/// <summary>
/// La clase Nickname gestiona la asignación y sincronización del apodo del jugador en una red con Photon.
/// </summary>
public class Nickname : MonoBehaviourPunCallbacks
{
    // Vista de Photon asociada al jugador
    private PhotonView pv;

    /// <summary>
    /// Obtiene o establece el PhotonView asociado al jugador.
    /// </summary>
    public PhotonView Pv
    {
        get { return pv; }
        set { pv = value; }
    }

    /// <summary>
    /// Inicializa la vista de Photon y asigna el apodo del jugador.
    /// </summary>
    public void Start() 
    {
        // Intentar obtener el componente PhotonView
        Pv = GetComponent<PhotonView>();
        
        if (Pv != null)
        {
            // Si Pv no es nulo, asignar el nombre del jugador
            Name(PhotonNetwork.NickName);
        }
        else
        {
            Debug.LogWarning("PhotonView no encontrado en el objeto.");
        }
    }

    /// <summary>
    /// Envía el nombre del jugador a todos los clientes a través de un RPC.
    /// </summary>
    /// <param name="name">El nombre del jugador.</param>
    public void Name(string name)
    {
        if (Pv == null)
        {
            Debug.LogError("PhotonView no está inicializado.");
            return;
        }

        if (string.IsNullOrEmpty(name))
        {
            Debug.LogWarning("El nombre es nulo o vacío.");
            return;
        }

        // Enviar el nombre a todos los clientes de forma sincronizada.
        Pv.RPC("RPC_Name", RpcTarget.AllBuffered, name);
    }

    /// <summary>
    /// RPC que sincroniza el nombre del jugador y lo muestra en un componente TextMesh.
    /// </summary>
    /// <param name="name">El nombre del jugador.</param>
    [PunRPC]
    public void RPC_Name(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogWarning("Nombre recibido en RPC es nulo o vacío.");
            return;
        }

        TextMesh textMesh = GetComponent<TextMesh>();
        
        if (textMesh != null)
        {
            // Si el TextMesh está presente, se actualiza el texto con el nombre del jugador.
            textMesh.text = name;
        }
        else
        {
            Debug.LogError("TextMesh no encontrado en el objeto.");
        }
    }

    // TODO: Considerar agregar una verificación si el jugador no tiene nombre y asignar un nombre predeterminado.
}

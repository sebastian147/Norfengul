using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagedelete : MonoBehaviour
{
    void Start()
    {
        // Kills the game object in 5 seconds after loading the object
        Destroy(gameObject, 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagedelete : MonoBehaviour
{
    //Damage value of the player, see when hit, this method is called to destroy it
    void Start()
    {
        // Kills the game object in 5 seconds after loading the object
        Destroy(gameObject, 2);
    }
}

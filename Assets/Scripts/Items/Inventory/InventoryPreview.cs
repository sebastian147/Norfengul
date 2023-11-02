using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPreview : MonoBehaviour
{
    public GameObject cuerpoPj;
    public GameObject peloPj;
    public GameObject barbaPj;
    public GameObject armaPj;
    public GameObject escudoPj;
    
    public GameObject cuerpoPw;
    public GameObject peloPw;
    public GameObject barbaPw;
    public GameObject armaPw;
    public GameObject escudoPw;

    public void Update ()
    {
       ChangeSpritePreview ();
    }

    public void ChangeSpritePreview ()
    {
        //Cuerpo
        Image cuerpoIm = cuerpoPw.GetComponent<Image>();
        SpriteRenderer cuerpoSp = cuerpoPj.GetComponent<SpriteRenderer>();

        if (cuerpoIm != null && cuerpoSp != null)
        {
            cuerpoIm.sprite = cuerpoSp.sprite;
        }
 
        //Pelo
        Image peloIm = peloPw.GetComponent<Image>();
        SpriteRenderer peloSp = peloPj.GetComponent<SpriteRenderer>();

        if (peloIm != null && peloSp != null)
        {
            peloIm.sprite = peloSp.sprite;
        }
        
        //Barba
        Image barbaIm = barbaPw.GetComponent<Image>();
        SpriteRenderer barbaSp = barbaPj.GetComponent<SpriteRenderer>();

        if (barbaIm != null && barbaSp != null)
        {
            barbaIm.sprite = barbaSp.sprite;
        }

        //Arma
        Image armaIm = armaPw.GetComponent<Image>();
        SpriteRenderer armaSp = armaPj.GetComponent<SpriteRenderer>();

        if (armaIm != null && armaSp != null)
        {
            if (armaSp.enabled)
            {
                armaIm.sprite = armaSp.sprite;
                armaIm.enabled = true;
            }
            else
            {
                armaIm.enabled = false;
            }
        }

        //Escudo
        Image escudoIm = escudoPw.GetComponent<Image>();
        SpriteRenderer escudoSp = escudoPj.GetComponent<SpriteRenderer>();

        if (escudoIm != null && escudoSp != null)
        {
            if (escudoSp.enabled)
            {
                escudoIm.sprite = escudoSp.sprite;
                escudoIm.enabled = true;
            }
            else
            {
                escudoIm.enabled = false;
            }
        }
    }
}
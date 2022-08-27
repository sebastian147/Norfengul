using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    Material material;

    bool isDissolving = false;
    double fade = 1f;

    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
       // material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            isDissolving = true;
        }

        if(isDissolving)
        {
            fade -=  Time.deltaTime*0.5;

            if(fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
            }
            
           // material.SetFloat("_Fade", (float)fade);
        }
    }
    public void Active()
    {
        active = true;
    }
}

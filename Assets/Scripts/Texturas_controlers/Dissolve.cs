using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dissolve : MonoBehaviour
{
    [SerializeField] GameObject[] t; 
    Material[] material;
    int i = 0;

    bool isDissolving = false;
    double fade = 1f;

    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        material = new Material[t.Length];
        foreach(GameObject e in t) 
        {
            try
            {
                material[i] = e.GetComponent<SpriteRenderer>().material;
            }
            catch(Exception s)
            {
                print(i);
                print(e);
                print(s);
            }
            i++;
        }
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
            for(int i = 0; i<t.Length;i++ ) 
            {
                material[i].SetFloat("_Fade", (float)fade);
            }
        }
    }
    public void Active()
    {
        active = true;
    }
}

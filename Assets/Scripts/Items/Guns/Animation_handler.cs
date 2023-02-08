using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animation_handler : MonoBehaviour
{
        //moveme de aca
        public event Action Onfinish;
        public void animationEnd()
        {
                Onfinish?.Invoke();
        }
}

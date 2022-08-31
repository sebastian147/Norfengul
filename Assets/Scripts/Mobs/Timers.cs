using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoBehaviour
{
    public bool timePassFixed(ref float time, float timeMax, bool start)
    {
        if(start)
        {
            time += Time.fixedDeltaTime;
            if(time>=timeMax)
            {
                time = 0;
                return true;
            }
            return false;
        }
        return true;
    }
}

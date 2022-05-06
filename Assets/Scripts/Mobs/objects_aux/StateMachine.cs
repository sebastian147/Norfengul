using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public Dictionary<int, MobBaseState> myDictionary = new Dictionary<int, MobBaseState>();

    /*
    Lista de estados
    0   IDLE
    1   WALK
    2   CROUCH 
    "Idle"

    */

    public enum myStates{
        Idle,
        Walk,
        Crouch
    }

    public MobBaseState initializeStates(){
        //Initilizes all mob states
        myDictionary.Add(0, new IdleState());
        myDictionary.Add(1, new WalkState());
        //myDictionary.Add(2, new CrouchState());
        
        //myStates.jumpSate = new JumpState();
        return myDictionary[0];
    }

    public MobBaseState changeState(int index)
    {
        //Returns the state needed by the mob
        return (MobBaseState)myDictionary[index];
    }
}

    
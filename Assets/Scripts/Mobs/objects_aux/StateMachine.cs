using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum myStates{
    Idle,
    Walk,
    Jump,
    Crouch
}
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



    public MobBaseState initializeStates(){
        //Initilizes all mob states
        myDictionary.Add((int) myStates.Idle, new IdleState());
        myDictionary.Add((int) myStates.Walk, new WalkState());
        myDictionary.Add((int) myStates.Jump, new CrouchState());
        
        //myStates.jumpSate = new JumpState();
        return myDictionary[0];
    }

    public MobBaseState changeState(int index)
    {
        //Returns the state needed by the mob
        return (MobBaseState)myDictionary[index];
    }
}

    
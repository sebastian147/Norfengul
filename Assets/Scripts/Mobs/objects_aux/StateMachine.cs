using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum myStates{
    Idle,
    Walk,
    Jump,
    Attack,
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
        myDictionary.Add((int) myStates.Jump, new OnJumpState());
        myDictionary.Add((int) myStates.Attack, new AttackState());
        
        //myStates.jumpSate = new JumpState();
        return myDictionary[0];
    }

    public int changeState(int index,int actual, Mob myMob)
    {
        myDictionary[actual].EndState(myMob);
        myDictionary[index].StarState(myMob);
        //Returns the state needed by the mob
        //return (MobBaseState)myDictionary[index];
        return index;
    }
}

    
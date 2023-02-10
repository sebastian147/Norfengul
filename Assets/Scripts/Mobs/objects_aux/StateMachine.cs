using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum myStates{
    Idle,
    Walk,
    Jump,
    Attack,
    WallGrabing,
    TakeDamage,
    //Crouch,
    Victory,
    Dash
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
        myDictionary.Add((int) myStates.WallGrabing, new WallJumpState());
        myDictionary.Add((int) myStates.TakeDamage, new TakeDamageState());
        myDictionary.Add((int) myStates.Victory, new VictoryState());
        myDictionary.Add((int) myStates.Dash, new DashState());

        //myStates.jumpSate = new JumpState();
        return myDictionary[0];
    }

    public int changeState(myStates index, Mob myMob)
    {
        myDictionary[myMob.actualState].EndState(myMob);
        myDictionary[(int) index].StarState(myMob);
        //Returns the state needed by the mob
        //return (MobBaseState)myDictionary[index];
        //Debug.Log((int) index);

        return (int) index;
    }
}

    
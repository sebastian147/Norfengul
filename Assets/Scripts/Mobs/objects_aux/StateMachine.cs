using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum myStates{
    Idle,
    Walk,
    Jump,
    Attack,
    WallGrabing,
    Victory,
    Dash,
    Damage,
    Running
}
public class StateMachine
{
    public Dictionary<int, MobBaseState> myDictionary = new Dictionary<int, MobBaseState>();

    /*
    Lista de estados
    0   IDLE
    1   WALK
    "Idle"

    */



    public MobBaseState initializeStates(){
        //Initilizes all mob states
        myDictionary.Add((int) myStates.Idle, new IdleState());
        myDictionary.Add((int) myStates.Walk, new WalkState());
        myDictionary.Add((int) myStates.Jump, new OnJumpState());
        myDictionary.Add((int) myStates.Attack, new AttackState());
        myDictionary.Add((int) myStates.WallGrabing, new WallJumpState());
        myDictionary.Add((int) myStates.Victory, new VictoryState());
        myDictionary.Add((int) myStates.Dash, new DashState());
        myDictionary.Add((int) myStates.Damage, new DamageState());
        myDictionary.Add((int) myStates.Running, new RunningState());

        //myStates.jumpSate = new JumpState();
        return myDictionary[0];
    }

    public int changeState(myStates index, Mob myMob)
    {

        myDictionary[myMob.actualState].EndState(myMob);
        myDictionary[(int) index].StartState(myMob);
        //Returns the state needed by the mob
        //return (MobBaseState)myDictionary[index];
        Debug.Log(Enum.GetName(typeof(myStates), index)+myMob.GetInstanceID());

        return (int) index;
    }
}

    
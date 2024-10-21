using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Enum que define los distintos estados que puede tener el mob.
/// </summary>
public enum myStates
{
    Idle,        // Estado de reposo
    Walk,        // Estado de caminar
    Jump,        // Estado de salto
    Attack,      // Estado de ataque
    WallGrabing, // Estado de agarrarse a la pared
    Victory,     // Estado de victoria
    Dash,        // Estado de desplazamiento rápido
    Damage,      // Estado de recibir daño
    Running      // Estado de correr
}

/// <summary>
/// Clase StateMachine que gestiona la transición entre diferentes estados del mob.
/// Utiliza un diccionario para almacenar instancias de estados específicos.
/// </summary>
public class StateMachine
{
    // Diccionario que almacena las instancias de los diferentes estados del mob
    public Dictionary<int, MobBaseState> myDictionary = new Dictionary<int, MobBaseState>();

    /// <summary>
    /// Inicializa todos los estados y los agrega al diccionario.
    /// Devuelve el estado inicial (Idle).
    /// </summary>
    /// <returns>El estado inicial del mob.</returns>
    public MobBaseState initializeStates()
    {
        // Se inicializan todos los estados del mob y se añaden al diccionario
        myDictionary.Add((int)myStates.Idle, new IdleState());
        myDictionary.Add((int)myStates.Walk, new WalkState());
        myDictionary.Add((int)myStates.Jump, new OnJumpState());
        myDictionary.Add((int)myStates.Attack, new AttackState());
        myDictionary.Add((int)myStates.WallGrabing, new WallJumpState());
        myDictionary.Add((int)myStates.Victory, new VictoryState());
        myDictionary.Add((int)myStates.Dash, new DashState());
        myDictionary.Add((int)myStates.Damage, new DamageState());
        myDictionary.Add((int)myStates.Running, new RunningState());

        // Devuelve el estado inicial (IdleState)
        return myDictionary[(int)myStates.Idle];
    }

    /// <summary>
    /// Cambia el estado actual del mob por uno nuevo.
    /// Finaliza el estado anterior y comienza el nuevo.
    /// </summary>
    /// <param name="index">El índice del nuevo estado a cambiar.</param>
    /// <param name="myMob">La instancia del mob que cambiará de estado.</param>
    /// <returns>El índice del estado nuevo.</returns>
    public int changeState(myStates index, Mob myMob)
    {
        // Termina el estado actual
        myDictionary[myMob.actualState].EndState(myMob);

        // Inicia el nuevo estado
        myDictionary[(int)index].StartState(myMob);

        // Registra el cambio de estado en la consola
        Debug.Log(Enum.GetName(typeof(myStates), index) + myMob.GetInstanceID());

        // Devuelve el índice del nuevo estado
        return (int)index;
    }
}

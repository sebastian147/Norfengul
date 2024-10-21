using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que maneja la victoria del mob, bloqueando otros estados mientras dure la animación de victoria.
/// </summary>
public class VictoryState : MobBaseState
{
    public string VICTORY_ANIMATION = "IsVictory"; // Nombre de la animación de victoria.
    public override void animate(Mob myMob)
    {
        // Activa la animación de victoria.
        myMob.myAnimator.SetBool(VICTORY_ANIMATION, true);
    }

    public override void EndState(Mob myMob)
    {
        // Desactiva la animación de victoria al salir del estado.
        myMob.myAnimator.SetBool(VICTORY_ANIMATION, false);
    }

    public override void StartState(Mob myMob)
    {
        // Inicia la animación de victoria.
        animate(myMob);
    }

    public override void CheckChangeState(Mob myMob)
    {
        // Evita el cambio de estado a menos que la victoria haya terminado.
        if (myMob.attacking)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Attack, myMob);
            return;
        }
        if (myMob.horizontalMove != 0 && myMob.running)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Running, myMob);
            return;
        }
        if (myMob.horizontalMove != 0)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Walk, myMob);
            return;
        }
        if (myMob.jumpBufferCounter > 0 || !myMob.m_Grounded)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump, myMob);
            return;
        }
        if (!myMob.victory)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle, myMob);
            return;
        }
    }

    public override void UpdateState(Mob myMob)
    {
        base.UpdateState(myMob);
        CheckChangeState(myMob);
    }

    public override void FixedUpdateState(Mob myMob)
    {
        // No requiere ajustes de físicas en este estado.
    }
}

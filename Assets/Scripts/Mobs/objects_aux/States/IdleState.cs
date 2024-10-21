using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado IdleState para manejar cuando el mob está en reposo (idle).
/// Comprueba si debe cambiar a otros estados según las entradas del jugador.
/// </summary>
public class IdleState : MobBaseState
{
    private string IDLE_ANIMATION = "idle";
    /// <summary>
    /// Activa la animación de idle en el mob.
    /// </summary>
    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetBool(IDLE_ANIMATION, true);
    }

    /// <summary>
    /// Finaliza el estado de idle, desactivando la animación.
    /// </summary>
    public override void EndState(Mob myMob)
    {
        myMob.myAnimator.SetBool(IDLE_ANIMATION, false);
    }

    /// <summary>
    /// Inicia el estado de idle activando la animación.
    /// </summary>
    public override void StartState(Mob myMob)
    {
        animate(myMob);
    }

    /// <summary>
    /// Comprueba las condiciones para cambiar a otros estados como Dash, Attack, Running, etc.
    /// </summary>
    public override void CheckChangeState(Mob myMob)
    {
        if ((myMob.dashRight || myMob.dashLeft) && myMob.canDash)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Dash, myMob);
            return;
        }
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
        if (myMob.victory)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Victory, myMob);
            return;
        }
    }

    /// <summary>
    /// Actualiza el estado de idle verificando si debe cambiar de estado.
    /// </summary>
    public override void UpdateState(Mob myMob)
    {
        base.UpdateState(myMob);
        CheckChangeState(myMob);
    }

    /// <summary>
    /// Aplica el movimiento suavizado cuando el mob está en estado de idle.
    /// </summary>
    public override void FixedUpdateState(Mob myMob)
    {
        Vector3 targetVelocity = new Vector2(myMob.horizontalMove * myMob.moveSpeed, myMob.myRigidbody.velocity.y);
        myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing);
    }
}

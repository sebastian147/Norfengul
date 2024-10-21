using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que maneja cuando el mob está caminando.
/// </summary>
public class WalkState : MobBaseState
{
    private string WALK_ANIMATION = "walking";

    public override void animate(Mob myMob)
    {
        // Activa la animación de caminar.
        myMob.myAnimator.SetBool(WALK_ANIMATION, true);
    }

    public override void EndState(Mob myMob)
    {
        // Desactiva la animación de caminar al salir del estado.
        myMob.myAnimator.SetBool(WALK_ANIMATION, false);
    }

    public override void StartState(Mob myMob)
    {
        // Activa la animación al iniciar el estado.
        animate(myMob);
    }

    public override void CheckChangeState(Mob myMob)
    {
        // Verifica si se debe cambiar de estado.
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
        if (myMob.jumpBufferCounter > 0 || !myMob.m_Grounded)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump, myMob);
            return;
        }
        if (myMob.horizontalMove != 0 && myMob.running)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Running, myMob);
            return;
        }
        if (Mathf.Abs(myMob.myRigidbody.velocity.x) < 1 && Mathf.Abs(myMob.horizontalMove) == 0)
        {
            // Cambia al estado de Idle si no hay movimiento horizontal.
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
        // Controla el movimiento del personaje suavizando la transición entre velocidades.
        Vector3 targetVelocity = new Vector2(myMob.horizontalMove * myMob.moveSpeed, myMob.myRigidbody.velocity.y);
        myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing);
    }
}

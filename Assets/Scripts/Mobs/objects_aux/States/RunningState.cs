using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado RunningState para el mob cuando está corriendo. Controla las animaciones y 
/// gestiona la transición entre otros estados según las condiciones.
/// </summary>
public class RunningState : MobBaseState
{
    public string RUN_ANIMATION = "running"; // Nombre de la animación de caminar.

    /// <summary>
    /// Activa la animación de correr.
    /// </summary>
    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetBool(RUN_ANIMATION, true);
    }

    /// <summary>
    /// Finaliza el estado de correr y desactiva la animación correspondiente.
    /// </summary>
    public override void EndState(Mob myMob)
    {
        myMob.myAnimator.SetBool(RUN_ANIMATION, false);
    }

    /// <summary>
    /// Inicia el estado de correr y llama a la animación correspondiente.
    /// </summary>
    public override void StartState(Mob myMob)
    {
        animate(myMob);
    }

    /// <summary>
    /// Verifica las condiciones para cambiar de estado, como el ataque o salto.
    /// </summary>
    public override void CheckChangeState(Mob myMob)
    {
        if ((myMob.dashRight || myMob.dashLeft) && myMob.canDash)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Dash, myMob);
            return;
        }
        if (myMob.attacking == true)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Attack, myMob);
            return;
        }
        if (myMob.jumpBufferCounter > 0 || !myMob.m_Grounded)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump, myMob);
            return;
        }
        if (myMob.horizontalMove != 0 && !myMob.running)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Walk, myMob);
            return;
        }
        if (Mathf.Abs(myMob.myRigidbody.velocity.x) < 1 && Mathf.Abs(myMob.horizontalMove) == 0)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle, myMob);
            return;
        }
    }

    /// <summary>
    /// Actualiza el estado de correr y revisa las condiciones de cambio de estado.
    /// </summary>
    public override void UpdateState(Mob myMob)
    {
        base.UpdateState(myMob);
        CheckChangeState(myMob);
    }

    /// <summary>
    /// Actualiza la física del estado de correr, ajustando la velocidad del mob.
    /// </summary>
    public override void FixedUpdateState(Mob myMob)
    {
        Vector3 targetVelocity = new Vector2(myMob.horizontalMove * myMob.runningSpeed, myMob.myRigidbody.velocity.y);
        // Suaviza y aplica la velocidad al mob.
        myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing); 
        /*TODO: Multiplicación de velocidades: El cálculo de la velocidad podría incluir ajustes dinámicos o interpolación, como un ajuste basado en 
        algún modificador o la interpolación entre la velocidad de caminar y correr, para evitar cambios bruscos en la velocidad del mob.*/
        /*TODO: verifica si puedes ajustar la cantidad de frames por segundo o el tiempo de actualización de las físicas para optimizar el 
        rendimiento. Es posible que no necesites calcular el suavizado cada frame si estás trabajando con movimientos constantes.*/
    }
}

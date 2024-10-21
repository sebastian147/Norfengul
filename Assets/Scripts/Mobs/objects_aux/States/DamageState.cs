using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado DamageState para manejar cuando el mob recibe daño.
/// Aplica un retroceso y cambia el layer a "Inmunity" para evitar recibir más daño durante el estado.
/// </summary>
public class DamageState : MobBaseState
{
    float diry = 0;
    private string HIT_ANIMATION = "isHit";

    /// <summary>
    /// Activa la animación de daño en el mob.
    /// </summary>
    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetBool(HIT_ANIMATION, true);
    }

    /// <summary>
    /// Finaliza el estado de daño, desactiva la animación y vuelve el layer del mob a "Player".
    /// </summary>
    public override void EndState(Mob myMob)
    {
        myMob.myAnimator.SetBool(HIT_ANIMATION, false);
        myMob.changeLayer("Player");
    }

    /// <summary>
    /// Inicia el estado de daño, resetea todas las animaciones y cambia el layer a "Inmunity".
    /// </summary>
    public override void StartState(Mob myMob)
    {
        AnimatorControllerParameter[] parametros = myMob.myAnimator.parameters;
        foreach (AnimatorControllerParameter parametro in parametros)
        {
            myMob.myAnimator.SetBool(parametro.name, false);
        }
        myMob.changeLayer("Inmunity");
    }

    /// <summary>
    /// Comprueba las condiciones para cambiar a otros estados como Attack, Running, Walk, etc.
    /// </summary>
    public override void CheckChangeState(Mob myMob)
    {
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
        if (Mathf.Abs(myMob.myRigidbody.velocity.x) < 1)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle, myMob);
            return;
        }
    }

    /// <summary>
    /// Actualiza el estado aplicando la animación de daño y chequeando el cambio de estado si la animación de hit termina.
    /// </summary>
    public override void UpdateState(Mob myMob)
    {
        animate(myMob);
        if (!(myMob.myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) && myMob.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            CheckChangeState(myMob);
        }
    }

    /// <summary>
    /// Aplica un retroceso (knockback) al mob basado en si está en el suelo o en el aire.
    /// </summary>
    public override void FixedUpdateState(Mob myMob)
    {
        diry = myMob.m_Grounded ? 1 : 0;
        Vector3 targetVelocity = new Vector2(myMob.dir * myMob.knockBackTake, diry * myMob.knockBackTake * 2);
        myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing);
        //si se aplica mucho knockback hacer fuerza y no velocidad
    }
}

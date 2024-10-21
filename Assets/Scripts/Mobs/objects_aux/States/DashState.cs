using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado DashState para manejar el movimiento de dash del mob.
/// Aplica un impulso de velocidad en la dirección del dash y modifica la gravedad temporalmente.
/// </summary>
public class DashState : MobBaseState
{
    float time;
    float originalGravity;
    private string DASH_ANIMATION = "isRoll";

    /// <summary>
    /// Activa la animación de rodar (dash).
    /// </summary>
    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetBool(DASH_ANIMATION, true);
    }

    /// <summary>
    /// Finaliza el estado de dash, restaurando la gravedad y desactivando la animación y el efecto de dash.
    /// </summary>
    public override void EndState(Mob myMob)
    {
        myMob.myRigidbody.gravityScale = originalGravity;
        myMob.tr.emitting = false;
        myMob.canDash = false;
        myMob.myAnimator.SetBool(DASH_ANIMATION, false);
        myMob.changeLayer("Player");
    }

    /// <summary>
    /// Inicia el estado de dash, aplicando el impulso de velocidad en la dirección correcta y activando la animación.
    /// </summary>
    public override void StartState(Mob myMob)
    {
        int direction = myMob.dashRight ? 1 : (myMob.dashLeft ? -1 : throw new System.Exception("error"));

        originalGravity = myMob.myRigidbody.gravityScale;
        myMob.changeLayer("Inmunity");

        myMob.myRigidbody.velocity = new Vector2(direction * myMob.dashingPower, 0f);
        time = myMob.dashingTime;
        Fliping(myMob);
        animate(myMob);
    }

    /// <summary>
    /// Comprueba las condiciones para cambiar de estado después de finalizar el dash.
    /// </summary>
    public override void CheckChangeState(Mob myMob)
    {
        if (time <= 0)
        {
            if (myMob.m_Grounded)
            {
                myMob.actualState = myMob.myStateMachine.changeState(myStates.Walk, myMob);
            }
            else
            {
                myMob.jumpsends = myMob.amountOfJumps;
                myMob.jumpdones = myMob.amountOfJumps;
                myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump, myMob);
            }
        }
    }

    /// <summary>
    /// Actualiza el estado verificando si la animación ha terminado para cambiar de estado.
    /// </summary>
    public override void UpdateState(Mob myMob)
    {
        if (!(myMob.myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1))
        {
            CheckChangeState(myMob);
        }
    }

    /// <summary>
    /// Resta el tiempo de dash a medida que pasa el tiempo en cada FixedUpdate.
    /// </summary>
    public override void FixedUpdateState(Mob myMob)
    {
        time -= Time.fixedDeltaTime;
    }

    /// <summary>
    /// Invierte la dirección del personaje si está mirando en la dirección opuesta al dash.
    /// </summary>
    public override void Fliping(Mob myMob)
    {
        if ((myMob.m_FacingRight && myMob.dashLeft) || (!myMob.m_FacingRight && myMob.dashRight))
        {
            Flip(myMob);
        }
    }
}

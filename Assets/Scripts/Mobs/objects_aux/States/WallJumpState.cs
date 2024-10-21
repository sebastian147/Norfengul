using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que maneja el salto en la pared. Controla la animación, las físicas, y verifica las condiciones para cambiar de estado.
/// </summary>
public class WallJumpState : MobBaseState
{
    private float timer; // Timer para manejar la duración del salto en la pared.
    private string WALL_ANIMATION = "isInWall";

    public override void animate(Mob myMob)
    {
        // Activa la animación de estar en la pared.
        myMob.myAnimator.SetBool(WALL_ANIMATION, true);
    }

    public override void EndState(Mob myMob)
    {
        // Finaliza el estado desactivando la animación y ajustando las propiedades de agarrado a la pared.
        myMob.myAnimator.SetBool(WALL_ANIMATION, false);
        myMob.wallGrabing = true;
    }

    public override void StartState(Mob myMob)
    {
        timer = 0f; // Resetea el timer al entrar en este estado.
        myMob.wallGrabingDirection = myMob.horizontalMove; // Guarda la dirección en la que está agarrando la pared.
        Fliping(myMob); // Ajusta la dirección del personaje según la pared.
        animate(myMob); // Activa la animación.
        myMob.myRigidbody.velocity = new Vector2(myMob.myRigidbody.velocity.x, 0); // Detiene el movimiento vertical del mob.
    }

    public override void CheckChangeState(Mob myMob)
    {
        // Verifica condiciones para cambiar de estado.
        if ((myMob.dashRight || myMob.dashLeft) && myMob.canDash)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Dash, myMob);
            return;
        }
        if (myMob.drop)
        {
            myMob.jumpsends = myMob.amountOfJumps;
            myMob.jumpdones = myMob.amountOfJumps;
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump, myMob);
            return;
        }
        if (myMob.horizontalMove != 0 && myMob.horizontalMove != myMob.wallGrabingDirection && myMob.jumpBufferCounter > 0)
        {
            // Si se intenta moverse en dirección opuesta a la pared, ejecuta un salto.
            myMob.wallGrabing = true;
            myMob.jumpsends = myMob.amountOfJumps - 1;
            myMob.jumpdones = myMob.amountOfJumps - 1;
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump, myMob);
            return;
        }
        if (timer > myMob.timeInwallBuffer)
        {
            // Si el tiempo en la pared excede el buffer, ejecuta un salto.
            myMob.jumpsends = myMob.amountOfJumps;
            myMob.jumpdones = myMob.amountOfJumps;
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump, myMob);
            return;
        }

        if (!myMob.m_Grounded && myMob.horizontalMove == myMob.wallGrabingDirection && !myMob._inWallLeft && !myMob._inWallRight)
        {
            myMob.jumpsends = myMob.amountOfJumps;
            myMob.jumpdones = myMob.amountOfJumps;
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump, myMob);
            return;
        }

        if (myMob.m_Grounded)
        {
            // Si aterriza en el suelo, cambia al estado de Idle.
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle, myMob);
            return;
        }
    }

    public override void UpdateState(Mob myMob)
    {
        // Actualiza el estado y el timer cuando el mob no está agarrado a la pared.
        if (myMob.horizontalMove != myMob.wallGrabingDirection)
        {
            timer += Time.fixedDeltaTime;
        }
        CheckChangeState(myMob); // Verifica las condiciones para cambiar de estado.
    }

    public override void FixedUpdateState(Mob myMob)
    {
        // Controla la velocidad de deslizamiento por la pared, limitando la velocidad vertical.
        myMob.myRigidbody.velocity = new Vector2(myMob.myRigidbody.velocity.x, Mathf.Clamp(myMob.myRigidbody.velocity.y, Vector2.down.y * myMob.wallSlidingSpeed, float.MaxValue));
    }

    public override void Fliping(Mob myMob)
    {
        // Asegura que el mob se voltee hacia la pared correctamente.
        if (myMob.m_FacingRight && myMob._inWallLeft)
        {
            Flip(myMob);
        }
        else if (!myMob.m_FacingRight && myMob._inWallRight)
        {
            Flip(myMob);
        }
    }
}

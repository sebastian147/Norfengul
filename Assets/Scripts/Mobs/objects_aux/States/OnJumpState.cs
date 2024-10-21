using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que controla el comportamiento de un mob durante un salto, 
/// incluyendo la lógica de salto normal, salto en pared, y corrección de esquinas.
/// </summary>
public class OnJumpState : MobBaseState
{
    private float originalGravity;        // Gravedad original del mob
    private bool jumpMade;                // Indica si el salto fue realizado
    private float timeInAir;              // Tiempo permitido en el aire (coyote time)
    private int timesEnter;               // Contador de entradas al estado (no utilizado actualmente)
    private float move;                   // Movimiento horizontal durante el salto
    private float jumpTimeFromWall = 0;   // Tiempo restante para saltar desde la pared
    private float jumpTimeFromWallMax = 0.4f; // Tiempo máximo para saltar desde la pared
    private float jumpTime = 0;           // Tiempo restante para un salto normal
    private float jumpTimeMax = 0.4f;     // Tiempo máximo para realizar un salto normal
    private CornerCorrection c = new CornerCorrection(); // Instancia para la corrección de esquinas
    private float moveSpeed = 0f;         // Velocidad de movimiento en el aire
    private string JUMP_ANIMATION = "isJumping";

    /// <summary>
    /// Inicia la animación de salto.
    /// </summary>
    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetBool(JUMP_ANIMATION, true);
    }

    /// <summary>
    /// Finaliza el estado de salto y restablece las propiedades del mob.
    /// </summary>
    public override void EndState(Mob myMob)
    {
        myMob.jumping = false;
        myMob.myAnimator.SetBool(JUMP_ANIMATION, false);

        // Restablece las propiedades de agarre en pared y modificadores de movimiento
        myMob.wallGrabingJumpforce = 0;
        myMob.wallGrabingDirection = 0;
        myMob.apexModifierCurrent = 1;
        myMob.apexModifierTimeCount = myMob.apexModifierTime;
        myMob.myRigidbody.gravityScale = originalGravity;
        myMob.wallGrabing = false;
    }

    /// <summary>
    /// Inicializa el estado de salto, configurando la fuerza de salto, tiempos, y verificaciones.
    /// </summary>
    public override void StartState(Mob myMob)
    {
        animate(myMob);  // Inicia la animación de salto
        originalGravity = myMob.myRigidbody.gravityScale;  // Guarda la gravedad original

        // Calcula la fuerza de salto basándose en la gravedad y la altura del salto
        myMob.m_JumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, myMob.jumpHeight);

        // Resetea las variables relacionadas con el salto
        jumpMade = false;
        myMob.timeInAir = myMob.allowedTimeInAir;
        jumpTime = jumpTimeMax;

        // Si está agarrado a la pared y no ha soltado, permite un salto desde la pared
        if (myMob.wallGrabing && !myMob.drop)
        {
            Flip(myMob);  // Voltea el mob para saltar desde la pared
            move = myMob.horizontalMove;
            jumpTimeFromWall = jumpTimeFromWallMax;
        }

        // Determina la velocidad en el aire según si está corriendo o caminando
        moveSpeed = myMob.running ? myMob.runningSpeed : myMob.moveSpeed;
    }

    /// <summary>
    /// Verifica si el mob debe cambiar de estado en función de su situación actual (pared, ataque, suelo, etc.).
    /// </summary>
    public override void CheckChangeState(Mob myMob)
    {
        // Si está en una pared y sigue moviéndose hacia ella, cambiar a estado de agarre en pared
        if (((myMob._inWallRight && myMob.horizontalMove > 0) || (myMob._inWallLeft && myMob.horizontalMove < 0)) 
            && !myMob.m_Grounded && !myMob.drop)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.WallGrabing, myMob);
            return;
        }

        // Si está atacando, cambiar al estado de ataque
        if (myMob.attacking)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Attack, myMob);
            return;
        }

        // Si toca el suelo y está en movimiento, cambiar a estado de caminar
        if (myMob.m_Grounded && Mathf.Abs(myMob.myRigidbody.velocity.x) > 1)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Walk, myMob);
            return;
        }

        // Si está en el suelo y no se mueve, cambiar a estado de inactividad
        if (myMob.m_Grounded)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle, myMob);
            return;
        }
    }

    /// <summary>
    /// Actualiza el estado en cada frame, manejando saltos, corrección de esquinas y cambios de estado.
    /// </summary>
    public override void UpdateState(Mob myMob)
    {
        if (!myMob.wallGrabing)  // Si no está agarrado a la pared, ejecuta la lógica normal de salto
        {
            base.UpdateState(myMob);
        }

        // Ejecuta el salto si está en el estado de salto
        if (myMob.jumping)
        {
            MakeAJump(myMob);
        }

        // Buffer para permitir saltar cuando toca el suelo después de un tiempo
        if (myMob.jumpBufferCounter > 0 && myMob.m_Grounded && myMob.jumpsends == 0)
        {
            myMob.jumpsends++;
            myMob.jumpBufferCounter = 0f;
        }

        jumpMade = JumpCheck(myMob);  // Verifica si el salto fue hecho
        c.CornerCorrectionAll(myMob);  // Aplica la corrección de esquinas si es necesario

        // Si el salto ha sido hecho o el tiempo de salto ha expirado, verifica el cambio de estado
        if (jumpMade || jumpTime <= 0)
        {
            CheckChangeState(myMob);
        }
        else
        {
            jumpTime -= Time.fixedDeltaTime;  // Reduce el tiempo disponible para realizar el salto
        }

        // Si el mob está intentando hacer un dash, cambia al estado de dash
        if ((myMob.dashRight || myMob.dashLeft) && myMob.canDash)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Dash, myMob);
        }
    }

    /// <summary>
    /// Actualiza la física del mob durante el estado de salto, ajustando la velocidad y la gravedad.
    /// </summary>
    public override void FixedUpdateState(Mob myMob)
    {
        // Ejecuta el salto si no ha completado todos los saltos permitidos
        if (myMob.jumpdones < myMob.jumpsends)
        {
            myMob.jumpdones++;
            myMob.myRigidbody.velocity = new Vector2(myMob.myRigidbody.velocity.x, 0);
            myMob.myRigidbody.AddForce(Vector2.up * myMob.m_JumpForce * myMob.myRigidbody.mass, ForceMode2D.Impulse);
        }

        // Si se detiene el salto antes de tiempo, aplica una fuerza contraria para detenerlo
        if (myMob.jumpStop && Vector2.Dot(myMob.myRigidbody.velocity, Vector2.up) > 0)
        {
            myMob.myRigidbody.AddForce(new Vector2(0f, -myMob.counterJumpForce) * myMob.myRigidbody.mass);
        }

        // Ajusta la gravedad y modifica el movimiento al llegar al ápice del salto
        if (Mathf.Abs(myMob.myRigidbody.velocity.y) < 0.15f && myMob.apexModifierTimeCount > 0)
        {
            myMob.apexModifierTimeCount -= Time.fixedDeltaTime;
            myMob.apexModifierCurrent = myMob.apexModifier;
            myMob.myRigidbody.gravityScale = 0;
            myMob.myRigidbody.velocity = new Vector3(myMob.myRigidbody.velocity.x, 0, 0);
        }
        else
        {
            myMob.apexModifierCurrent = 1;
            myMob.apexModifierTimeCount = myMob.apexModifierTime;
            myMob.myRigidbody.gravityScale = originalGravity;
        }

        // Reduce el tiempo en el aire (coyote time)
        if (!myMob.m_Grounded && myMob.timeInAir > 0)
        {
            myMob.timeInAir -= Time.fixedDeltaTime;
        }

        // Maneja el salto desde la pared
        if (myMob.wallGrabing && jumpTimeFromWall >= 0)
        {
            myMob.horizontalMove = move;
            jumpTimeFromWall -= Time.fixedDeltaTime;
        }
        else
        {
            myMob.wallGrabing = false;
        }

        // Mueve el mob en el aire
        Vector3 targetVelocity = new Vector2(myMob.horizontalMove * moveSpeed * myMob.apexModifierCurrent, myMob.myRigidbody.velocity.y);
        myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing);
    }

    /// <summary>
    /// Realiza un salto adicional si es posible (incluyendo saltos en el aire y coyote time).
    /// </summary>
    private void MakeAJump(Mob myMob)
    {
        if (myMob.jumpsends < myMob.amountOfJumps)  // Saltos adicionales permitidos
        {
            myMob.jumpsends++;
            myMob.jumpBufferCounter = 0f;
        }

        if (myMob.jumpsends == 1 && !myMob.m_Grounded && myMob.timeInAir <= 0)  // Coyote time
        {
            myMob.jumpsends = 0;
            myMob.jumpBufferCounter = 0f;
        }

        myMob.jumping = false;  // Termina el salto
    }

    /// <summary>
    /// Calcula la fuerza de salto basada en la gravedad y la altura de salto deseada.
    /// </summary>
    private static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    /// <summary>
    /// Verifica si el mob está en el aire (salto hecho).
    /// </summary>
    private bool JumpCheck(Mob myMob)
    {
        if (!myMob.m_Grounded)
        {
            jumpMade = true;
        }
        return jumpMade;
    }
}

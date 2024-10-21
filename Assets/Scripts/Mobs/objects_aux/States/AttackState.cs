using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado de ataque para un mob. Se encarga de manejar la animación de ataque, 
/// detectar enemigos y jugadores en el área de daño, y aplicar el daño correspondiente.
/// </summary>
public class AttackState : MobBaseState
{
    private string attackType = "";  // Tipo de ataque basado en el arma equipada
    private float attackDirection;   // Dirección del ataque
    private HashSet<int> hitPlayerIDs; // Conjunto de IDs para los jugadores golpeados (friendly fire)

    /// <summary>
    /// Ejecuta la animación de ataque usando el tipo de arma del mob.
    /// </summary>
    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetBool(attackType, true);  // Activa la animación de ataque basada en el tipo de arma
    }

    /// <summary>
    /// Finaliza el estado de ataque y restablece la animación.
    /// </summary>
    public override void EndState(Mob myMob)
    {
        myMob.myAnimator.SetBool(attackType, false);  // Desactiva la animación de ataque
    }

    /// <summary>
    /// Inicializa el estado de ataque, incluyendo el tipo de arma, dirección del ataque 
    /// y comienza a verificar enemigos a golpear.
    /// </summary>
    public override void StartState(Mob myMob)
    {
        hitPlayerIDs = new HashSet<int>();  // Inicializa el conjunto de jugadores golpeados
        attackDirection = myMob.horizontalMove;  // Asigna la dirección del ataque
        attackType = myMob.arma.Armas.weaponType.ToString();  // Obtiene el tipo de arma
        myMob.attacking = false;  // Desactiva la flag de ataque
        animate(myMob);  // Inicia la animación de ataque
        CheckEnemysToAttack(myMob);  // Verifica enemigos a golpear
    }

    /// <summary>
    /// Cambia de estado si las condiciones se cumplen (correr, caminar, saltar, etc.).
    /// </summary>
    public override void CheckChangeState(Mob myMob)
    {
        // Cambia a estado de correr si está en movimiento y corriendo
        if (myMob.horizontalMove != 0 && myMob.running)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Running, myMob);
            return;
        }

        // Cambia a estado de caminar si está en movimiento pero no corriendo
        if (myMob.horizontalMove != 0)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Walk, myMob);
            return;
        }

        // Cambia a estado de salto si el buffer de salto está activo o no está en el suelo
        if (myMob.jumpBufferCounter > 0 || !myMob.m_Grounded)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump, myMob);
            return;
        }

        // Cambia a estado de descanso si no está en movimiento
        if (Mathf.Abs(myMob.horizontalMove) == 0)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle, myMob);
            return;
        }
    }

    /// <summary>
    /// Actualiza el estado verificando si debe cambiar y chequeando si se golpean jugadores.
    /// </summary>
    public override void UpdateState(Mob myMob)
    {
        // Chequea si se golpea jugadores en un rango específico de la animación
        if (myMob.myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.85f && 
            myMob.myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.55f)
        {
            CheckPlayersToAttack(myMob);
        }

        // Verifica si el ataque ha terminado para cambiar de estado
        if (!(myMob.myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f))
        {
            CheckChangeState(myMob);
        }
    }

    /// <summary>
    /// Actualiza la física del estado, ajustando la velocidad del mob durante el ataque.
    /// </summary>
    public override void FixedUpdateState(Mob myMob)
    {
        // Ajusta la velocidad horizontal durante el ataque, reduciendo el movimiento
        Vector3 targetVelocity = new Vector2(myMob.horizontalMove * myMob.moveSpeed / 4, myMob.myRigidbody.velocity.y);
        myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing);
    }

    /// <summary>
    /// Verifica si hay jugadores en el área de ataque y les aplica daño si corresponde.
    /// </summary>
    private void CheckPlayersToAttack(Mob myMob)
    {
        // Solo se ejecuta si está activado el friendly fire
        if (myMob.friendlyFire)
        {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(myMob.attackPoint.position, myMob.arma.Armas.damageArea, myMob.playerLayers);

            foreach (Collider2D player in hitPlayer)
            {
                int hitPlayerID = player.gameObject.GetInstanceID();

                // Si el jugador golpeado no es el propio mob y no ha sido golpeado antes
                if (hitPlayerID != myMob.gameObject.GetInstanceID() && !hitPlayerIDs.Contains(hitPlayerID))
                {
                    hitPlayerIDs.Add(hitPlayerID);  // Añade el jugador a la lista de golpeados
                    player.GetComponent<Mob>().TakeDamage(myMob.arma.Armas.damage, myMob.m_FacingRight, myMob.arma.Armas.knockback);  // Aplica daño y knockback
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Verifica si hay enemigos en el área de ataque y les aplica daño si corresponde.
    /// </summary>
    private void CheckEnemysToAttack(Mob myMob)
    {
        // Detecta enemigos en el área de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(myMob.attackPoint.position, myMob.arma.Armas.damageArea, myMob.enemyLayers);

        // Aplica daño a los enemigos detectados (lógica futura para enemigos)
        foreach (Collider2D enemy in hitEnemies)
        {
            // enemy.GetComponent<Enemy>().TakeDamage(myMob.arma.Armas.damage);
        }
    }
    /*TODO Hit Tracking: Usé HashSet<int> para evitar golpes repetidos a los mismos jugadores, optimizando la detección.
                Animación: Ajusté las verificaciones de animación para garantizar que el ataque solo se detecte en momentos específicos de la animación.
                Física: Reducí la velocidad del personaje mientras ataca, ajustando el movimiento durante la animación.*/
}

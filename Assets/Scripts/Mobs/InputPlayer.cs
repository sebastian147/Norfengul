using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer
{
    // Variables para gestionar doble toque para dash
    float DoubleTapTimeL = 0f; // Tiempo restante para realizar un doble toque hacia la izquierda
    float DoubleTapTimeMaxL = 2f; // Tiempo máximo permitido para el doble toque izquierdo
    int DoubleTapCounterL = 0; // Contador de toques hacia la izquierda
    float DoubleTapTimeR = 0f; // Tiempo restante para realizar un doble toque hacia la derecha
    float DoubleTapTimeMaxR = 2f; // Tiempo máximo permitido para el doble toque derecho
    int DoubleTapCounterR = 0; // Contador de toques hacia la derecha

    /// <summary>
    /// Verifica las entradas del jugador para diferentes acciones como saltar, atacar, moverse, correr, hacer dash y caer.
    /// </summary>
    public void InputChecks(Mob myMob, MeleWeaponLogic myMeleWeaponLogic)
    {
        JumpCheck(myMob); // Verifica si el jugador intenta saltar
        AttackCheck(myMob, myMeleWeaponLogic); // Verifica si el jugador intenta atacar
        MoveCheck(myMob); // Verifica si el jugador se está moviendo
        myMob.victory = ChangeBoolStateInput("v"); // Verifica si el jugador activa el estado de victoria
        myMob.dashLeft = ChangeBoolStateInput("q"); // Verifica si el jugador activa el dash hacia la izquierda
        myMob.dashRight = ChangeBoolStateInput("e"); // Verifica si el jugador activa el dash hacia la derecha
        myMob.running = IsRunning(myMob); // Verifica si el jugador está corriendo
        DropCheck(myMob); // Verifica si el jugador está cayendo
    }

    /// <summary>
    /// Cambia el estado booleano de acuerdo a la tecla presionada.
    /// </summary>
    public bool ChangeBoolStateInput(string key)
    {
        return Input.GetKey(key); // Devuelve true si se presiona la tecla, false si no.
    }

    /// <summary>
    /// Verifica si el jugador está corriendo (usando Shift izquierdo o derecho).
    /// </summary>
    public bool IsRunning(Mob myMob)
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    /// <summary>
    /// Verifica las entradas relacionadas con el salto. Maneja el buffer de salto y el control de cuándo debe detenerse.
    /// </summary>
    public void JumpCheck(Mob myMob)
    {
        if (Input.GetButtonDown("Jump")) // Cuando se presiona el botón de salto
        {
            myMob.jumpBufferCounter = myMob.jumpBufferTime; // Inicia el contador del buffer de salto
            myMob.jumpStop = false; // Indica que no se ha detenido el salto
            myMob.jumping = true; // Marca que el jugador está intentando saltar

            if (myMob.jumpdones == 0) // Resetea el contador de saltos cuando no se ha realizado ninguno
            {
                myMob.jumpsends = 0;
            }
        }
        else if (Input.GetButtonUp("Jump")) // Cuando se suelta el botón de salto
        {
            myMob.jumpStop = true; // Indica que se ha detenido el salto
        }
        else if (myMob.jumpBufferCounter > 0 && myMob.jumpStop) // Gestiona el tiempo restante en el buffer de salto
        {
            myMob.jumpBufferCounter -= Time.fixedDeltaTime; // Reduce el contador del buffer de salto
        }
    }

    /// <summary>
    /// Verifica si el jugador está atacando con el arma equipada. Solo se ejecuta si el arma está disponible y el tiempo de ataque ha pasado.
    /// </summary>
    public void AttackCheck(Mob myMob, MeleWeaponLogic myMeleWeaponLogic)
    {
        if (Input.GetMouseButtonDown(0) && myMeleWeaponLogic.Armas != null && Time.time >= myMob.nextAttackTime) // Verifica si el jugador intenta atacar
        {
            myMob.nextAttackTime = Time.time + 1f / myMob.attackRate; // Actualiza el tiempo para el siguiente ataque
            myMob.attacking = true; // Marca que el jugador está atacando
        }
    }

    /// <summary>
    /// Verifica la entrada de movimiento horizontal del jugador.
    /// </summary>
    public void MoveCheck(Mob myMob)
    {
        myMob.horizontalMove = Input.GetAxisRaw("Horizontal"); // Actualiza la variable horizontalMove con el valor de la entrada horizontal.\\
    }

    /// <summary>
    /// Verifica si el jugador está intentando caer (empujar hacia abajo en el eje vertical).
    /// </summary>
    public void DropCheck(Mob myMob)
    {
        myMob.drop = Input.GetAxisRaw("Vertical") < 0; // Establece true si el jugador está empujando hacia abajo en el eje vertical.
    }

    /// <summary>
    /// Verifica si se ha hecho un doble toque en la tecla especificada. 
    /// Esta función puede ser usada para activar un dash o una acción especial.
    /// </summary>
    public bool DoubleTap(Mob myMob, string key, ref float DoubleTapTime, float DoubleTapTimeMax, ref int DoubleTapCounter)
    {
        if(Input.GetButtonDown(key))
        {
            if(DoubleTapCounter == 0)
            {
                DoubleTapTime = DoubleTapTimeMax;
            }
            DoubleTapCounter++;
        }

        if(DoubleTapCounter > 0)
        {
            DoubleTapTime -= Time.fixedDeltaTime;

            if(DoubleTapTime <= 0)
            {
                DoubleTapCounter = 0;
            }
        }

        if(DoubleTapCounter >= 2)
        {
            DoubleTapCounter = 0;
            return true;
        }
        
        return false;
    }
}

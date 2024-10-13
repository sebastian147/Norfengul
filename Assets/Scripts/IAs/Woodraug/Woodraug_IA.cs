using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodraug_IA : MonoBehaviour
{
    public Animator animator;
    public GameObject Player;

    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float Life = 20;

    // Estados
    private enum State { Idle, Opening, Open, Closing, Attack, Dead }
    private State currentState;

    void Start()
    {
        // Inicialmente en Idle
        currentState = State.Idle;

        // Inicializa los parámetros del Animator
        animator.SetBool("Open", false);
        animator.SetBool("IsOpening", false);
        animator.SetBool("IsClosing", false);
        animator.SetBool("Dead", false);
        animator.SetBool("Atack", false);
    }

    void Update()
    {
        if (Life <= 0)
        {
            SetState(State.Dead);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        // Cambiar de estado según la distancia al jugador
        if (currentState == State.Opening && IsAnimationFinished("Opening"))
        {
            SetState(State.Open);  // Cambia automáticamente a Open cuando termine Opening
        }
        else if (distanceToPlayer <= attackRange)
        {
            SetState(State.Attack);
        }
        else if (distanceToPlayer <= detectionRange)
        {
            if (currentState == State.Idle || currentState == State.Closing)
            {
                SetState(State.Opening);
            }
        }
        else
        {
            if (currentState == State.Open || currentState == State.Opening)
            {
                SetState(State.Closing);
            }
            else
            {
                SetState(State.Idle);
            }
        }

        HandleState();
    }

    // Cambiar de estado y actualizar las animaciones
    void SetState(State newState)
    {
        if (currentState == newState) return; // Evitar cambios innecesarios

        currentState = newState;

        switch (currentState)
        {
            case State.Idle:
                animator.SetBool("Atack", false);
                animator.SetBool("IsOpening", false);
                animator.SetBool("IsClosing", false);
                break;

            case State.Opening:
                animator.SetBool("IsOpening", true);
                animator.SetBool("Open", false);
                animator.SetBool("IsClosing", false);
                break;

            case State.Open:
                animator.SetBool("Open", true);
                animator.SetBool("IsOpening", false);
                animator.SetBool("IsClosing", false);
                break;

            case State.Closing:
                animator.SetBool("IsClosing", true);
                animator.SetBool("IsOpening", false);
                break;

            case State.Attack:
                animator.SetBool("Open", true);
                animator.SetBool("Atack", true);
                break;

            case State.Dead:
                animator.SetBool("Atack", false);
                animator.SetBool("IsOpening", false);
                animator.SetBool("IsClosing", false);
                animator.SetBool("Open", false);
                animator.SetBool("Dead", true);
                break;
        }
    }

    // Lógica del estado actual
    void HandleState()
    {
        switch (currentState)
        {
            case State.Idle:
                // No hace nada en Idle
                break;

            case State.Opening:
                // Se abre: puedes agregar lógica adicional aquí si es necesario
                break;

            case State.Open:
                // Estado en el que se queda abierto
                ChasePlayer(); // Persigue al jugador
                break;

            case State.Closing:
                // Se cierra: puedes agregar lógica adicional aquí si es necesario
                break;

            case State.Attack:
                AttackPlayer(); // Ataca al jugador
                break;

            case State.Dead:
                // No hacer nada si está muerto
                break;
        }
    }

    // Función para perseguir al jugador
    void ChasePlayer()
    {
        // Lógica para moverse hacia el jugador
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * 2f);
    }

    // Función para atacar al jugador
    void AttackPlayer()
    {
        Debug.Log("Atacando al jugador");
    }

    void OnOpeningComplete()
    {
        SetState(State.Open);
    }

    bool IsAnimationFinished(string animationName)
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animStateInfo.IsName(animationName) && animStateInfo.normalizedTime >= 1.0f;
    }
}
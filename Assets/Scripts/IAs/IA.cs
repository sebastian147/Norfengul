using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    public float moveSpeed = 5;
    public Vector2 origenPos;
    private Animator anim;

    void Start()
    {
        origenPos = transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(transform.position.x != origenPos.x)
        {
            MoveTo(origenPos);
        }
        else 
        {
            Idle();
        }
    }

    void Idle()
    {
        anim.SetBool("IsMoving", false);
    }

    void MoveTo(Vector2 target)
    {
        anim.SetBool("IsMoving", true);
        Vector2 targetPosition = new Vector2(target.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}

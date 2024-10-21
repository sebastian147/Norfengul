using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase abstracta base para los estados de un mob. Proporciona la estructura general y métodos comunes
/// para gestionar la lógica de los estados. Cada estado hereda de esta clase y sobrescribe los métodos para 
/// implementar su comportamiento específico.
/// </summary>
public abstract class MobBaseState
{
    /// <summary>
    /// Método virtual para actualizar el estado. Por defecto, se encarga de verificar el volteo del mob.
    /// </summary>
    /// <param name="myMob">El mob cuyo estado está siendo actualizado.</param>
    public virtual void UpdateState(Mob myMob)
    {
        Fliping(myMob); // Llama a la función de volteo según la dirección del movimiento.
    }

    // Métodos abstractos que cada estado debe implementar.
    public abstract void animate(Mob  myMob);
    public abstract void EndState(Mob myMob);
    public abstract void StartState(Mob myMob);
    public abstract void CheckChangeState(Mob myMob);
    public abstract void FixedUpdateState(Mob myMob);

    /// <summary>
    /// Método virtual opcional para cambiar de estado. Puede ser sobrescrito por los estados hijos si es necesario.
    /// </summary>
    /// <param name="myMob">El mob cuyo estado podría cambiar.</param>
    public virtual void SwitchState(Mob myMob) {}

    /// <summary>
    /// Verifica si el mob debe voltear su orientación basada en la dirección del movimiento.
    /// </summary>
    /// <param name="myMob">El mob cuya dirección debe ser verificada.</param>
    public virtual void Fliping(Mob myMob)
    {
        // Si el mob se mueve hacia la derecha pero está mirando hacia la izquierda.
        if (myMob.horizontalMove > 0 && !myMob.m_FacingRight)
        {
            Flip(myMob); // Voltea el mob hacia la derecha.
        }
        // Si el mob se mueve hacia la izquierda pero está mirando hacia la derecha.
        else if (myMob.horizontalMove < 0 && myMob.m_FacingRight)
        {
            Flip(myMob); // Voltea el mob hacia la izquierda.
        }
    }

    /// <summary>
    /// Voltea el mob cambiando su escala en el eje X.
    /// </summary>
    /// <param name="myMob">El mob que debe ser volteado.</param>
    protected void Flip(Mob myMob)
    {
        // Cambia la dirección en la que el mob está mirando.
        myMob.m_FacingRight = !myMob.m_FacingRight;

        // Multiplica la escala local en X por -1 para voltear.
        Vector3 theScale = myMob.transform.localScale;
        theScale.x *= -1;
        myMob.transform.localScale = theScale;

        // También ajusta la escala del nickname.
        myMob.nickName.transform.localScale = theScale;
    }
}

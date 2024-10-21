using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que gestiona las actualizaciones de colisiones del mob, como la detección de suelo, paredes y zonas de muerte.
/// </summary>
public class CollisionUpdates : MonoBehaviour
{
    /// <summary>
    /// Método que ejecuta todas las comprobaciones de colisiones.
    /// </summary>
    public void CollisionCheck(Mob myMob)
    {
        // Comprueba si el mob está en una zona de muerte
        IsDeathZoneCheck(myMob);
        // Comprueba si el mob está tocando el suelo
        IsGroundedCheck(myMob);
        // Comprueba si el mob está en contacto con una pared
        IsWallCheck(myMob);
    }

    /// <summary>
    /// Comprueba si el mob está en contacto con una pared.
    /// </summary>
    private void IsWallCheck(Mob myMob)
    {
        // Raycasts para detectar la pared en la izquierda
        RaycastHit2D raycastParedLeftTop = Physics2D.Raycast(myMob.m_WallCheck.position, new Vector2(-1, 0), myMob._wallRayCastLenght, myMob.m_WhatIsWall);
        RaycastHit2D raycastParedLeftDown = Physics2D.Raycast(myMob.m_WallCheck.position + new Vector3(0, -myMob.distanceFromGrabs, 0), new Vector2(-1, 0), myMob._wallRayCastLenght, myMob.m_WhatIsGround);
        // Raycasts para detectar la pared en la derecha
        RaycastHit2D raycastParedRightTop = Physics2D.Raycast(myMob.m_WallCheck.position, new Vector2(1, 0), myMob._wallRayCastLenght, myMob.m_WhatIsWall);
        RaycastHit2D raycastParedRightDown = Physics2D.Raycast(myMob.m_WallCheck.position + new Vector3(0, -myMob.distanceFromGrabs, 0), new Vector2(1, 0), myMob._wallRayCastLenght, myMob.m_WhatIsGround);

        // Si ambos rayos detectan una pared en la izquierda
        if (raycastParedLeftTop && raycastParedLeftDown)
        {
            myMob._inWallLeft = true;
        }
        else
        {
            myMob._inWallLeft = false;
        }

        // Si ambos rayos detectan una pared en la derecha
        if (raycastParedRightTop && raycastParedRightDown)
        {
            myMob._inWallRight = true;
        }
        else
        {
            myMob._inWallRight = false;
        }
    }

    /// <summary>
    /// Comprueba si el mob está en una zona de muerte.
    /// </summary>
    public void IsDeathZoneCheck(Mob myMob)
    {
        // Comprueba si el mob está en una zona de muerte utilizando un círculo de colisión
        Collider2D[] collidersD = Physics2D.OverlapCircleAll(myMob.m_GroundCheck.position, myMob.k_GroundedRadius, myMob.m_whatIsDeath);

        // Si colisiona con una zona de muerte, aplica daño máximo
		foreach (var collider in collidersD)
		{
			// Si encuentra una zona de muerte, aplica el daño máximo y rompe el bucle
			myMob.TakeDamage(147483647, false, 0);
			break; // Detiene la búsqueda después del primer collider detectado
		}
    }

    /// <summary>
    /// Comprueba si el mob está tocando el suelo.
    /// </summary>
    public void IsGroundedCheck(Mob myMob)
    {
        // Raycasts hacia abajo desde diferentes posiciones para comprobar si está tocando el suelo
        RaycastHit2D raycastSuelo = Physics2D.Raycast(myMob.m_GroundCheck.position, Vector2.down, myMob._groundRayCastLenght, myMob.m_WhatIsGround);
        RaycastHit2D raycastSuelo2 = Physics2D.Raycast(myMob.m_GroundCheck.position - new Vector3(myMob.offset, 0, 0), Vector2.down, myMob._groundRayCastLenght, myMob.m_WhatIsGround);
        RaycastHit2D raycastSuelo3 = Physics2D.Raycast(myMob.m_GroundCheck.position + new Vector3(myMob.offset, 0, 0), Vector2.down, myMob._groundRayCastLenght, myMob.m_WhatIsGround);

        // Si cualquiera de los rayos detecta el suelo, el mob está en el suelo
        if (raycastSuelo || raycastSuelo2 || raycastSuelo3)
        {
            myMob.m_Grounded = true;
            
            // Resetea el contador de saltos si estaba en el aire
            if (myMob.jumpdones != 0)
            {
                myMob.jumpsends = 0;
            }
            myMob.jumpdones = 0; // Reinicia los saltos realizados
        }
        else
        {
            myMob.m_Grounded = false;
        }
    }
}

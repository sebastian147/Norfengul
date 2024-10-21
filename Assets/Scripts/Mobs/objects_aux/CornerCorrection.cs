using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase CornerCorrection que ajusta la posición del mob para corregir colisiones en las esquinas, tanto en la parte superior como inferior.
/// </summary>
public class CornerCorrection
{
    /// <summary>
    /// Ejecuta la corrección de esquina tanto en la parte superior como inferior.
    /// </summary>
    /// <param name="myMob">El mob que se está corrigiendo.</param>
    public void CornerCorrectionAll(Mob myMob)
    {
        // Ejecuta la corrección en la parte superior
        CornerCorrectionTop(myMob);
        // Opción adicional para ejecutar la corrección en la parte superior con tamaño (comentada por defecto)
        // CornerCorrectionTopSize(myMob);
        // Ejecuta la corrección en la parte inferior
        CornerCorrectionbottom(myMob);
    }

    /// <summary>
    /// Corrección de esquina en la parte superior con un tamaño ajustable.
    /// Ajusta la posición del mob si detecta una colisión en la parte superior.
    /// </summary>
    public void CornerCorrectionTopSize(Mob myMob)
    {
        // Raycast hacia la izquierda desde diferentes posiciones
        RaycastHit2D raycastSueloLeft = Physics2D.Raycast(myMob.m_CeilingCheck.position - new Vector3(myMob.distanceFromMidle, -myMob.offsetInB, 0), Vector2.left, myMob._topRayCastLenghtB, myMob.m_WhatIsGround);
        RaycastHit2D raycastSueloLeft2 = Physics2D.Raycast(myMob.m_CeilingCheck.position - new Vector3(myMob.distanceFromMidle, -myMob.offsetOutB, 0), Vector2.left, myMob._topRayCastLenghtB, myMob.m_WhatIsGround);
        // Raycast hacia la derecha desde diferentes posiciones
        RaycastHit2D raycastSueloRight = Physics2D.Raycast(myMob.m_CeilingCheck.position + new Vector3(myMob.distanceFromMidle, myMob.offsetInB, 0), Vector2.right, myMob._topRayCastLenghtB, myMob.m_WhatIsGround);
        RaycastHit2D raycastSueloRight2 = Physics2D.Raycast(myMob.m_CeilingCheck.position + new Vector3(myMob.distanceFromMidle, myMob.offsetOutB, 0), Vector2.right, myMob._topRayCastLenghtB, myMob.m_WhatIsGround);

        // Ajusta la posición hacia la izquierda si detecta una colisión específica
        if ((!raycastSueloLeft && raycastSueloLeft2) && (!raycastSueloRight && !raycastSueloRight2) && !myMob.m_Grounded && myMob.myRigidbody.velocity.x < 0)
        {
            myMob.transform.position += new Vector3(-0.1f, 0.1f, 0);
        }
        // Ajusta la posición hacia la derecha si detecta una colisión específica
        else if ((!raycastSueloLeft && !raycastSueloLeft2) && (!raycastSueloRight && raycastSueloRight2) && !myMob.m_Grounded && myMob.myRigidbody.velocity.x > 0)
        {
            myMob.transform.position += new Vector3(0.1f, 0.1f, 0);
        }
		//checkfunctionality
    }

    /// <summary>
    /// Corrección de esquina en la parte superior.
    /// </summary>
    public void CornerCorrectionTop(Mob myMob)
    {
        // Raycast hacia arriba desde diferentes posiciones
        RaycastHit2D raycastSueloLeft = Physics2D.Raycast(myMob.m_CeilingCheck.position - new Vector3(myMob.offsetIn, 0, 0), Vector2.up, myMob._topRayCastLenght, myMob.m_WhatIsGround);
        RaycastHit2D raycastSueloLeft2 = Physics2D.Raycast(myMob.m_CeilingCheck.position - new Vector3(myMob.offsetOut, 0, 0), Vector2.up, myMob._topRayCastLenght, myMob.m_WhatIsGround);
        RaycastHit2D raycastSueloRight = Physics2D.Raycast(myMob.m_CeilingCheck.position + new Vector3(myMob.offsetIn, 0, 0), Vector2.up, myMob._topRayCastLenght, myMob.m_WhatIsGround);
        RaycastHit2D raycastSueloRight2 = Physics2D.Raycast(myMob.m_CeilingCheck.position + new Vector3(myMob.offsetOut, 0, 0), Vector2.up, myMob._topRayCastLenght, myMob.m_WhatIsGround);

        // Ajusta la posición si no hay colisión en la izquierda pero hay en la derecha
        if ((!raycastSueloLeft && raycastSueloLeft2) && (!raycastSueloRight && !raycastSueloRight2))
        {
            myMob.transform.position += new Vector3(myMob.offsetOut - myMob.offsetIn, 0, 0);
        }
        // Ajusta la posición en el caso inverso
        else if ((!raycastSueloLeft && !raycastSueloLeft2) && (!raycastSueloRight && raycastSueloRight2))
        {
            myMob.transform.position -= new Vector3(myMob.offsetOut - myMob.offsetIn, 0, 0);
        }
    }

    /// <summary>
    /// Corrección de esquina en la parte inferior.
    /// </summary>
    public void CornerCorrectionbottom(Mob myMob)
    {
        // Raycast hacia la izquierda desde diferentes posiciones
        RaycastHit2D raycastSueloLeft = Physics2D.Raycast(myMob.m_GroundCheck.position - new Vector3(myMob.distanceFromMidle, -myMob.offsetInB, 0), Vector2.left, myMob._topRayCastLenghtB, myMob.m_WhatIsGround);
        RaycastHit2D raycastSueloLeft2 = Physics2D.Raycast(myMob.m_GroundCheck.position - new Vector3(myMob.distanceFromMidle, -myMob.offsetOutB, 0), Vector2.left, myMob._topRayCastLenghtB, myMob.m_WhatIsGround);
        // Raycast hacia la derecha desde diferentes posiciones
        RaycastHit2D raycastSueloRight = Physics2D.Raycast(myMob.m_GroundCheck.position + new Vector3(myMob.distanceFromMidle, myMob.offsetInB, 0), Vector2.right, myMob._topRayCastLenghtB, myMob.m_WhatIsGround);
        RaycastHit2D raycastSueloRight2 = Physics2D.Raycast(myMob.m_GroundCheck.position + new Vector3(myMob.distanceFromMidle, myMob.offsetOutB, 0), Vector2.right, myMob._topRayCastLenghtB, myMob.m_WhatIsGround);

        // Ajusta la posición hacia la izquierda o derecha dependiendo de las colisiones detectadas
        if ((!raycastSueloLeft && raycastSueloLeft2) && (!raycastSueloRight && !raycastSueloRight2) && !myMob.m_Grounded && myMob.myRigidbody.velocity.x < 0)
        {
            myMob.transform.position += new Vector3(-0.1f, myMob.offsetOutB-myMob.offsetInB, 0);
        }
        else if ((!raycastSueloLeft && !raycastSueloLeft2) && (!raycastSueloRight && raycastSueloRight2) && !myMob.m_Grounded && myMob.myRigidbody.velocity.x > 0)
        {
            myMob.transform.position += new Vector3(0.1f, myMob.offsetOutB-myMob.offsetInB, 0);
        }
    }
}

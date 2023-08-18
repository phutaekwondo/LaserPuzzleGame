using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : LaserInteractable
{
    Vector3 m_mirrorNormal = Vector3.zero;

    private void Update() 
    {
        m_mirrorNormal = this.transform.forward;
    }

    override public bool IsBounce(Vector3 incomingDirection)
    {
        float dot = Vector3.Dot(incomingDirection, m_mirrorNormal);
        return dot < 0;
    }
    
    override public Vector3 GetBounceDirection(Vector3 incomingDirection)
    {
        if (IsBounce(incomingDirection))
        {
            return Vector3.Reflect(incomingDirection, m_mirrorNormal);
        }
        else
        {
            throw new System.Exception("Laser is hitting the wrong side of the mirror");
        }
    }
}

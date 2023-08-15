using UnityEngine;

public class LaserInteractable : MonoBehaviour
{
    virtual public Vector3 BounceDirection(Vector3 incomingDirection)
    {
        return this.transform.forward;
    }
}

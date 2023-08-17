using UnityEngine;

public class LaserInteractable : MonoBehaviour
{
    virtual public Vector3 GetBounceDirection(Vector3 incomingDirection)
    {
        return this.transform.forward;
    }
}

using UnityEngine;

abstract public class LaserInteractable : MonoBehaviour
{
    abstract public bool IsBounce(Vector3 incomingDirection);
    virtual public Vector3 GetBounceDirection(Vector3 incomingDirection)
    {
        return this.transform.forward;
    }
}

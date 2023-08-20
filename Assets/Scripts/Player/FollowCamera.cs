using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform m_target;

    Vector3 offset = new Vector3(14f, 20f, -14f); 

    private void Update() 
    {
        transform.position = m_target.position + offset;
    }
}

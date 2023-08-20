using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform m_target;
    Vector3 m_offset = new Vector3(14f, 20f, -14f); 
    float m_height = 20f;

    private void Start() 
    {
        transform.position = m_target.position + m_offset;
    }
    private void Update() 
    {
        transform.position = m_target.position + m_offset;
        transform.position = new Vector3(transform.position.x, m_height, transform.position.z);
    }
}

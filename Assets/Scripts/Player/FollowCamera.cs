using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform m_target;

    Vector3 m_targetPrePosition;

    private void Start() 
    {
        m_targetPrePosition = m_target.position;
    }
    private void Update() 
    {
        Vector3 movement = m_target.position - m_targetPrePosition;
        transform.position += movement;
        m_targetPrePosition = m_target.position;
    }
}

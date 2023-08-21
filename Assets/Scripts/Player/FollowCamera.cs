using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform m_target;
    Vector3 m_offset = new Vector3(14f, 20f, -14f); 
    float m_height = 20f;
    [SerializeField] float m_maxDelayDistance = 5f;
    [SerializeField] float m_maxCamSpeed = 3f;
    [SerializeField] float m_minCamSpeed = 1f;

    private void Start() 
    {
        transform.position = m_target.position + m_offset;
        transform.position = new Vector3(transform.position.x, m_height, transform.position.z);
    }
    private void Update() 
    {
        Vector3 destination = m_target.position + m_offset;
        destination.y = transform.position.y;
        Vector3 moveDir = (destination - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, destination);

        if (distance > m_maxDelayDistance)
        {
            transform.position = destination - moveDir * (m_maxDelayDistance * 0.99f);
        }
        else
        {
            Vector3 movement = moveDir * Mathf.Lerp(m_minCamSpeed, m_maxCamSpeed, distance / m_maxDelayDistance) * Time.deltaTime;
            transform.position += movement;
        }
    }
}

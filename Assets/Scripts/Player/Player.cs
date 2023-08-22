using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] float m_speed = 1f;
    [SerializeField] float m_jumpForce = 100f;
    [SerializeField] Collider m_footCollider;
    Vector3 m_rightDirection;
    private Rigidbody m_rb;

    private bool m_isGrounded = false;

    private void Awake() {
        m_rb = GetComponent<Rigidbody>();
        m_rightDirection = transform.right;
    }

    void Update()
    {
        MovementUpdate();
        JumpUpdate();
    }

    private void OnCollisionEnter(Collision other) 
    {
        ContactPoint[] contacts = other.contacts;
        for (int i = 0; i < contacts.Length; i++)
        {
            ContactPoint contact = contacts[i];
            if (contact.thisCollider == m_footCollider)
            {
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        ContactPoint[] contacts = other.contacts;
        for (int i = 0; i < contacts.Length; i++)
        {
            ContactPoint contact = contacts[i];
            if (contact.thisCollider == m_footCollider)
            {
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        m_isGrounded = false; // TODO: check if we are still on the ground in OnCollisionStay
    }


    private void JumpUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_rb.AddForce(Vector3.up * m_jumpForce);
        }
    }

    protected void MovementUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forwardDirection = new Vector3(-m_rightDirection.z, 0, m_rightDirection.x);

        Vector3 movement = (m_rightDirection * horizontal + forwardDirection * vertical).normalized * m_speed * Time.deltaTime;
        m_rb.MovePosition(transform.position + movement);

        if (movement.magnitude > 0)
        {
            this.transform.forward = movement.normalized;
        }
    }
}

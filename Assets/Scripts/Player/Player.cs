using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    // [SerializeField] protected Camera m_camera;
    [SerializeField] float m_mouseSentivity = 1f;
    [SerializeField] float m_speed = 1f;
    [SerializeField] float m_jumpForce = 100f;
    [SerializeField] Collider m_footCollider;
    private Rigidbody m_rb;

    private bool m_isGrounded = false;

    private void Awake() {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CameraAngleUpdate();
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

    protected void CameraAngleUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // this.m_camera.transform.Rotate(-mouseY * this.m_mouseSentivity, 0, 0);
        this.transform.Rotate(0, mouseX * this.m_mouseSentivity, 0);
    }
    protected void MovementUpdate()
    {
        float side = Input.GetAxis("Horizontal");
        float straight = Input.GetAxis("Vertical");

        Vector3 horizontalMove = this.transform.right * side + this.transform.forward * straight;
        horizontalMove.Normalize();
        horizontalMove *= this.m_speed * Time.deltaTime;

        this.m_rb.MovePosition(this.transform.position + horizontalMove);

        // Vector3 currentVelocity = this.m_rb.velocity;
        // currentVelocity.x = 0;
        // currentVelocity.z = 0;

        // Vector3 targetVelocity = currentVelocity + horizontalMove;
        // this.m_rb.velocity = targetVelocity;
    }
}

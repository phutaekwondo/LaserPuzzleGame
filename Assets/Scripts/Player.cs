using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected Camera m_camera;
    [SerializeField] float m_mouseSentivity = 1f;
    [SerializeField] float m_speed = 1f;
    private Rigidbody m_rb;
    PlayerState m_state;

    private void Awake() {
        m_state = new PlayerState(this);
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_state.Update();
    }

    public class PlayerState
    {
        protected Player m_player;

        public PlayerState(Player player)
        {
            m_player = player;
        }
        protected void CameraAngleUpdate()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            m_player.m_camera.transform.Rotate(-mouseY * m_player.m_mouseSentivity, 0, 0);
            m_player.transform.Rotate(0, mouseX * m_player.m_mouseSentivity, 0);
        }
        protected void HandleWalkInput()
        {
            float side = Input.GetAxis("Horizontal");
            float straight = Input.GetAxis("Vertical");

            Vector3 horizontalMove = m_player.transform.right * side + m_player.transform.forward * straight;
            horizontalMove.Normalize();
            horizontalMove *= m_player.m_speed * Time.deltaTime;

            Debug.Log(horizontalMove);

            Vector3 currentVelocity = m_player.m_rb.velocity;
            currentVelocity.x = 0;
            currentVelocity.z = 0;

            Vector3 targetVelocity = currentVelocity + horizontalMove;
            m_player.m_rb.velocity = targetVelocity;
        }

        public virtual void Update()
        {
            CameraAngleUpdate();
            HandleWalkInput();
        }
    }
}

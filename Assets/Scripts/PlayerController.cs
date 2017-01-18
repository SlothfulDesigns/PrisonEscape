using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    #region public figures

    public int hitpoints = 100;
    public int ammo = 6;

    public float runSpeed = 10.0f;
    public float jumpSpeed = 8.0f;

    #endregion

    #region private parts

    private Vector3 m_lookDirection;
    private Vector3 m_moveDirection = Vector3.zero;
    private Vector3 m_mouseLook;

    private InputController m_inputController;
    private CharacterController m_playerController;
    private CharacterController m_droneController;

    private Rigidbody m_rigidBody;

    private Weapon m_weapon;

    private GameObject m_drone;
    private Camera m_playerCamera;
    private Camera m_droneCamera;

    private float m_vSpeed = 0.0f;

    private bool m_alive = false;
    
    #endregion

    // Use this for initialization
    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        m_rigidBody = this.GetComponent<Rigidbody>();

        m_inputController = GetComponent<InputController>();

        m_playerController = this.GetComponent<CharacterController>();
        m_playerCamera = this.GetComponentInChildren<Camera>();
        m_playerCamera.enabled = true;
        m_lookDirection = transform.forward;
        m_mouseLook = transform.forward;

        m_weapon = this.GetComponentInChildren<Weapon>();

        m_drone = GameObject.Find("Drone");
        m_droneCamera = m_drone.GetComponentInChildren<Camera>();
        m_droneCamera.enabled = false;
        m_droneController = m_drone.GetComponent<CharacterController>();
        m_droneController.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {

        /**********/
        /* MOVING */
        /**********/

        // Lookment
        m_lookDirection = Quaternion.AngleAxis(m_inputController.Mouse().x, Vector3.up) * m_lookDirection;
        transform.rotation = Quaternion.LookRotation(m_lookDirection, Vector3.up);
        m_playerCamera.transform.rotation *= Quaternion.Euler(-m_inputController.Mouse().y, 0.0f, 0.0f);

        // Movement
        var m = m_inputController.GetMovement();
        Vector3 m_move = transform.forward * m.y + transform.right * m.x;

        if (m_playerController.isGrounded)
        {
            if (m_inputController.Jump())
            {
                m_vSpeed = jumpSpeed;
            }
        }

        m_vSpeed -= 9.81f * Time.deltaTime;

        m_moveDirection.z = m_move.z * runSpeed;
        m_moveDirection.x = m_move.x * runSpeed;
        m_moveDirection.y = m_vSpeed;

        m_playerController.Move(m_moveDirection * Time.deltaTime);

        /***********/
        /* ACTIONS */
        /***********/
        if (m_inputController.IsFiring())
        {
            if (m_weapon != null)
            {
                m_weapon.Shoot();
            }
        }

        // Switch to drone
        if (Input.GetButtonDown("DroneSwitch"))
        {
            // Switch cameras
            m_playerCamera.enabled = false;
            m_droneCamera.enabled = true;

            //Switch controls
            m_droneController.enabled = true;
        }
    }

    public bool IsMoving()
    {
        return m_moveDirection.sqrMagnitude > 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        var pickup = other.GetComponent<Pickup>();
        if(pickup != null)
        {
            Debug.Log("Picked up: " + pickup.ammo + " ammo");
            this.ammo += pickup.ammo;
            pickup.Take();
        }
    }
}

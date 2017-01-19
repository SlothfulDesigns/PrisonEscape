using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(CharacterController))]
public class DroneController : MonoBehaviour
{

    public float flySpeed = 10.0f;
    public float climbSpeed = 8.0f;

    private InputController _inputController;
    private CharacterController _characterController;
    private DroneController _droneController;
    private PlayerController _playerController;

    private Camera _playerCamera;
    private Camera _droneCamera;
    
    private Vector3 m_lookDirection;
    private Vector3 m_moveDirection = Vector3.zero;
    private float _vSpeed = 0.0f;

    private GameObject _player;

	// Use this for initialization
	void Start ()
    {

        _inputController = GetComponent<InputController>();
        _characterController = this.GetComponent<CharacterController>();

        _player = GameObject.Find("Player");
        _playerCamera = _player.GetComponentInChildren<Camera>();
        _playerController = _player.GetComponent<PlayerController>();

        _droneCamera = this.GetComponentInChildren<Camera>();
        _droneController = this.GetComponent<DroneController>();
        _droneController.enabled = false;

        m_lookDirection = transform.forward;
    }
	
	// Update is called once per frame
	void Update ()
    {

        /**********/
        /* MOVING */
        /**********/

        // Lookment
        //m_lookDirection = Quaternion.AngleAxis(_inputController.Mouse().x, Vector3.up) * m_lookDirection;
        //transform.rotation = Quaternion.LookRotation(m_lookDirection, Vector3.up);

        // Movement
        var m = _inputController.GetMovement();
        Vector3 m_move = transform.forward * m.y + transform.right * m.x;

        m_moveDirection.z = m_move.z * flySpeed;
        m_moveDirection.x = m_move.x * flySpeed;
        m_moveDirection.y = _vSpeed;

        _characterController.Move(m_moveDirection * Time.deltaTime);

        // Switch to drone
        if (Input.GetButtonDown("DroneSwitch"))
        {
            // Switch cameras
            _playerCamera.enabled = true;
            _droneCamera.enabled = false;

            // Switch controls
            _droneController.enabled = false;
            _playerController.enabled = true;

            // Disable cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public bool IsMoving()
    {
        return m_moveDirection.sqrMagnitude > 0.0f;
    }
}

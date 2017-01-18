using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {

    private DroneController _droneController;
    private PlayerController _playerController;

    private Camera _playerCamera;
    private Camera _droneCamera;

    private GameObject _player;

	// Use this for initialization
	void Start () {
        _player = GameObject.Find("Drone");
        _playerCamera = _player.GetComponentInChildren<Camera>();
        _playerController = _player.GetComponent<PlayerController>();

        _droneCamera = this.GetComponentInChildren<Camera>();
        _droneController = this.GetComponent<DroneController>();
        _droneController.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Switch to drone
        if (Input.GetButtonDown("DroneSwitch"))
        {
            // Switch cameras
            _playerCamera.enabled = true;
            _droneCamera.enabled = false;

            //Switch controls
            _droneController.enabled = false;
            _playerController.enabled = true;
        }
    }
}

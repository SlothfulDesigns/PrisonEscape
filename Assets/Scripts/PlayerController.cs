using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    #region public figures

    public float mouseSensitivityX = 10.0f;
    public float mouseSensitivityY = 10.0f;

    public float moveSpeed = 10.0f;
    public float jumpSpeed = 8.0f;

    public float gravity = 9.807f;

    #endregion

    #region private parts

    private float _positionX = 0.0f;
    private float _positionY = 0.0f;
    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;

    #endregion

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Look();
        Move();
    }

    private void Move()
    {
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");

        transform.localPosition += 0.01f * (new Vector3(h, 0.0f, v) * moveSpeed);
    }

    private void Look()
    {
        _rotationX += Input.GetAxis("Mouse X") * (mouseSensitivityX * 0.1f);
        _rotationY += Input.GetAxis("Mouse Y") * (mouseSensitivityY * 0.1f);

        transform.localRotation = Quaternion.AngleAxis(_rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(_rotationY, Vector3.left);
    }

    private void Shoot(int weapon)
    {

    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Camera))]
public class PlayerController : MonoBehaviour
{

    #region public figures

    public int hitpoints = 100;

    #endregion

    #region private parts

    private CharacterController _cc;
    private Camera _camera;
    private Weapon _weapon;

    private bool _alive = false;

    #endregion

    // Use this for initialization
    void Start ()
    {
        _cc = this.GetComponent<CharacterController>();
        _camera = this.GetComponent<Camera>();
        _weapon = this.GetComponentInChildren<Weapon>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1")){
            if (_weapon != null)
            {
                Debug.Log("Shooting " + _weapon.name);
                _weapon.Shoot();
            }
        }
    }
}

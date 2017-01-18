using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector2 m_Look = Vector2.zero;
    private Vector2 m_Move = Vector2.zero;
    private bool m_Jump;
    private bool m_Fire;

    void Start ()
    {
		
	}
	
    public Vector2 Mouse()
    {
        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");
        m_Look = new Vector2(x, y);
        return m_Look;
    }

    public Vector2 GetMovement()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        m_Move = new Vector2(h, v);

        if(m_Move.sqrMagnitude > 1)
        {
            m_Move.Normalize();
        }

        return m_Move;
    }

    public bool Jump()
    {
        return Input.GetButtonDown("Jump");
    }

    public bool IsFiring()
    {
        return Input.GetButton("Fire1");
    }

    public bool IsMoving()
    {
        return (m_Move.x + m_Move.y) > 0.0F;
    }
}

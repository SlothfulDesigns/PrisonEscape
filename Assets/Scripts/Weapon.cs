using UnityEngine;

public class Weapon : MonoBehaviour {

    public int damage = 100;
    public float fireRate = 1.0f; // shots per second if you hold down the buttan

    private int _ammo; // quick reference to player's ammo count
    private float _lastShot = 0.0f;

    // objects'n'shit
    private PlayerController _player;
    private Camera _camera;
    private GameObject _barrel;

    // head bobbing things
    private Vector3 _bobMidpoint;
    private float _bobSpeed = 0.2f;
    private float _bobAmountX = 0.002f;
    private float _bobAmountY = 0.005f;
    private float _bobAmountZ = 0.001f;
    private float _bobTimer = 0.0f;
    private float _bobWave = 0.0f;

    // audio FX
    private AudioSource _audioSource;
    private AudioClip _shotSfx;
    private AudioClip _click;

    // visual FX 
    private GameObject _shotVfx;
    private GameObject _hitVfx;
    private LineRenderer _lineRenderer;


	// Use this for initialization
	private void Start () {
        // basic stuff
        _player = this.GetComponentInParent<PlayerController>();
        _camera = Camera.main;
        _bobMidpoint = this.transform.localPosition;
        _barrel = GameObject.Find("Barrel");

        // SFX
        _audioSource = this.GetComponent<AudioSource>();
        _shotSfx = Resources.Load<AudioClip>("SHOT");
        _click = Resources.Load<AudioClip>("Click");

        // VFX
        _shotVfx = Resources.Load<GameObject>("Flare");
        _hitVfx = Resources.Load<GameObject>("Flare");

        _lastShot = fireRate; // so we can start shooting right away
	}

    private void FixedUpdate()
    {
        _lastShot += Time.deltaTime;
    }

    // Update is called once per frame
    private void Update ()
    {
        WeaponSway();
	}

    private bool CanShoot()
    {
        var canShoot = true;

        // check firerate
        if (_lastShot < fireRate) return false;

        return canShoot;
    }

    // swing the wepan around while we go
    private void WeaponSway()
    {
        var h = Mathf.Abs(Input.GetAxis("Horizontal"));
        var v = Mathf.Abs(Input.GetAxis("Vertical"));

        if (h == 0.0f && v == 0.0f)
        {
            _bobTimer = 0.0f;
        }
        else
        {
            _bobWave = Mathf.Sin(_bobTimer);
            _bobTimer = _bobTimer + _bobSpeed;
            if (_bobTimer > Mathf.PI * 2.0f)
            {
                _bobTimer = _bobTimer - (Mathf.PI * 2.0f);
            }
        }

        if (_bobWave != 0.0f)
        {
            var axes = Mathf.Clamp(h + v, 0.0f, 1.0f);
            var swayX = (_bobWave * _bobAmountX * axes);
            var swayY = (_bobWave * _bobAmountY * axes);
            var swayZ = (_bobWave * _bobAmountZ * axes);
            var offset = new Vector3(swayX, swayY, swayZ);
            this.transform.localPosition += offset;
        }
        else
        {
            this.transform.localPosition = _bobMidpoint;
        }
    }

    public void Shoot()
    {

        if (!CanShoot()) return;

        if (_player.ammo > 0 && CanShoot())
        {
            _lastShot = 0.0f;
            _player.ammo--;
            _audioSource.PlayOneShot(_shotSfx);

            var vfx = Instantiate(_shotVfx, _barrel.transform.position, _barrel.transform.rotation);
            vfx.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            Destroy(vfx, 0.05f);

            var rc = new Ray(_camera.transform.position, _camera.transform.forward);
            var hit = new RaycastHit();

            if (Physics.Raycast(rc, out hit))
            {

                var enemy = hit.collider.GetComponentInParent<Enemy>();
                if (enemy != null && !enemy.dead)
                {
                    enemy.Damage(this.damage);
                }
                var hitVfx = Instantiate(_hitVfx, hit.point, Quaternion.identity);
                Destroy(hitVfx, 0.1f);
            }
        }
        else
        {
            if(Input.GetButtonDown("Fire1"))
            {
                _audioSource.PlayOneShot(_click);
            }
        }
    }
}

﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public int ammo = 10;
    public int damage = 100;

    private AudioSource _audioSource;
    private Camera _camera;
    private LineRenderer _lineRenderer;

    private AudioClip _shotSfx;
    private AudioClip _click;
    private GameObject _shotVfx;
    private GameObject _hitVfx;

    private GameObject _barrel;

	// Use this for initialization
	void Start () {
        _camera = Camera.main;
        _audioSource = this.GetComponent<AudioSource>();

        _barrel = GameObject.Find("Barrel");

        _shotSfx = Resources.Load<AudioClip>("SHOT");
        _shotVfx = Resources.Load<GameObject>("Smoke");
        _hitVfx = Resources.Load<GameObject>("Flare");

        _lineRenderer = this.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Shoot()
    {
        if (ammo > 0)
        {
            ammo--;
            _audioSource.PlayOneShot(_shotSfx);
            var vfx = Instantiate(_shotVfx, _barrel.transform.position, _barrel.transform.rotation);
            vfx.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            Destroy(vfx, 1.0f);
            var rc = new Ray(_barrel.transform.position, _camera.transform.forward);
            var hit = new RaycastHit();

            if (Physics.Raycast(rc, out hit))
            {

                var enemy = hit.collider.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    Debug.Log("Hit " + enemy.name + " with " + this.name + " for " + this.damage + " damage.");
                    enemy.Damage(this.damage);
                }
                var hitVfx = Instantiate(_hitVfx, hit.point, Quaternion.identity);
                Destroy(hitVfx, 0.1f);
            }
        }
        else {
            _audioSource.PlayOneShot(_click);

        }
    }
}

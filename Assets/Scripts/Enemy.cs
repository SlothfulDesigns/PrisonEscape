﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private AudioClip _ded;
    private AudioSource _audioSource;

    public bool dead;
    public int hitpoints = 100;


	// Use this for initialization
	void Start ()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _ded = Resources.Load<AudioClip>("dead");
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if (!dead && hitpoints <= 0)
        {
            kill();
        }
	}

    public void Damage(int damage)
    {
        if (!this.dead)
        {
            this.hitpoints -= damage;

            if (this.hitpoints <= 0)
            {
                this.kill();
            }
        }
    }

    private void kill()
    {
        if (!this.dead)
        {
            Debug.Log(this.name + " ded.");
            this.dead = true;
            _audioSource.PlayOneShot(_ded);

            // destroy object after one sec
            Destroy(this.gameObject, 1);
        }
    }
}

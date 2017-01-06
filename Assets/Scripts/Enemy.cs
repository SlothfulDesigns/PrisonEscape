using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private AudioClip _ded;
    private AudioSource _audioSource;

    public bool dead;
    public int hitpoints = 100;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () {

        if (!dead && hitpoints <= 0) {
            kill();
        }
        //play sound before destroying
        if (dead && !_audioSource.isPlaying) {
            Destroy(this.gameObject);
        }
	}

    public void Damage(int damage) {
        this.hitpoints -= damage;
    }
    private void kill(){
        _audioSource.PlayOneShot(_ded);
        this.dead = true;
    }
}

using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    private AudioSource _audioSource;
    private AudioClip _shootFx;

	// Use this for initialization
	void Start () {
        _audioSource = this.GetComponent<AudioSource>();
        _shootFx = Resources.Load<AudioClip>("SHOT");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Shoot()
    {
        _audioSource.PlayOneShot(_shootFx);
    }
}

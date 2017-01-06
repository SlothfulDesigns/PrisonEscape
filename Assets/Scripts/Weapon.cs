using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public int damage = 100;

    private AudioSource _audioSource;
    private AudioClip _shootFx;
    private Camera _camera;

	// Use this for initialization
	void Start () {
        _camera = Camera.main;
        _audioSource = this.GetComponent<AudioSource>();
        _shootFx = Resources.Load<AudioClip>("SHOT");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Shoot()
    {
        _audioSource.PlayOneShot(_shootFx);

        var rc = new Ray(_camera.transform.position, _camera.transform.forward);
        var hit = new RaycastHit();
        if(Physics.Raycast(rc, out hit))
        {
            var enemy = hit.collider.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Hit " + enemy.name + " with " + this.name + " for " + this.damage + " damage.");
                enemy.Damage(this.damage);
            }
        }
    }
}

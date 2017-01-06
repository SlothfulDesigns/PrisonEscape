using UnityEngine;

public class Pickup : MonoBehaviour {


    public int ammo;
    public int health; //lol no
    public string pickupSfx = "DefaultPickup";


    private BoxCollider _collider;
    private AudioSource _audioSource;
    private AudioClip _pickupSfx;

    // Use this for initialization
    void Start () {
        _collider = this.GetComponent<BoxCollider>();
        _audioSource = this.GetComponent<AudioSource>();
        _pickupSfx = Resources.Load<AudioClip>(pickupSfx);
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.Rotate(new Vector3(0.0f, this.transform.rotation.z + 1));
	}

    public void Take()
    {
        _collider.enabled = false;
        _audioSource.PlayOneShot(_pickupSfx);
        Destroy(gameObject, 1);
    }
}

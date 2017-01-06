using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () {

        if (!dead && hitpoints <= 0) {
            kill();
        }
        //play sound before destroying
        if (dead && !AudioSource.isPlaying) {
            Destroy(this.gameObject);
        }
	}

    public void Damage(int damage) {
        this.hitpoints -= damage;
    }
    private void kill(){
        TriggerSoundEffect(itemSoundFx.Dead);
        this.dead = true;
    }
}

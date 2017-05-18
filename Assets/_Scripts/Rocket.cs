using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField]
    private GameObject _Explosion;
    [SerializeField]
    private AudioClip _ExplosionSound;
    [SerializeField]
    private AudioSource _ExplosionHolder;

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource explosionS = Instantiate(_ExplosionHolder, transform.position, transform.rotation) as AudioSource;
        explosionS.clip = _ExplosionSound;
        explosionS.Play();
        Instantiate(_Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}

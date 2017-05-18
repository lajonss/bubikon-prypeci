using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class Rocket : MonoBehaviour {
    [SerializeField]
    private GameObject _Explosion;
    [SerializeField]
    private AudioClip _ExplosionSound;
    [SerializeField]
    private AudioSource _ExplosionHolder;
    [SerializeField]
    private float _Radius;
    [SerializeField]
    private float _Force;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.CompareTo("RigidBodyFPSController") != 0)
        {
            var colliders = Physics.OverlapSphere(transform.position, _Radius);
            foreach(var collider in colliders)
            {
                var rigidBody = collider.GetComponent<Rigidbody>();
                if (rigidBody != null)
                {
                    var dist = collider.gameObject.transform.position - transform.position;
                    var force = dist.normalized * _Force / (dist.magnitude + 0.1f) * Random.Range(0.5f, 2f);
                    
                    rigidBody.AddForce(force, ForceMode.Impulse);
                }
            }
            AudioSource explosionS = Instantiate(_ExplosionHolder, transform.position, transform.rotation) as AudioSource;
            explosionS.clip = _ExplosionSound;
            explosionS.Play();
            Instantiate(_Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class Rocket : MonoBehaviour {
    #region Inspector Variables
    [SerializeField]
    private GameObject _Explosion;
    [SerializeField]
    private AudioClip _ExplosionSound;
    [SerializeField]
    private AudioSource _ExplosionHolder;
    [SerializeField]
    private float _Radius;
    [SerializeField]
    private float _DamageRadius = 1.0f;
    [SerializeField]
    private float _Force;
    [SerializeField]
    private float _Damage = 100.0f;
    #endregion Inspector Variables

    private float _ExplosionTime = 2.5f;

    private Renderer _Renderer;
    private Collider _Collider;

    private AudioSource _ExplosionSource;
    private GameObject _ExplosionObject;
    
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
            _ExplosionSource = Instantiate(_ExplosionHolder, transform.position, transform.rotation) as AudioSource;
            _ExplosionSource.clip = _ExplosionSound;
            _ExplosionSource.Play();
            _ExplosionObject = Instantiate(_Explosion, transform.position, transform.rotation);
            _Renderer.enabled = false;
            _Collider.enabled = false;
            Invoke("CleanUp", _ExplosionTime);
        }
    }

    private void CleanUp()
    {
        Destroy(_ExplosionSource.gameObject);
        Destroy(_ExplosionObject);
        Destroy(gameObject);
    }

    void Start () {
        _Renderer = gameObject.GetComponent<Renderer>();
        _Collider = gameObject.GetComponent<Collider>();
	}
	
	void Update () {
        var message = new MessageTypes.Damage()
        {
            // Why this works
            // But Value = _Damage * Time.deltaTime; Send always 1 dmg instead of 100?
            Value = 100.0f * Time.deltaTime,
            Sender = this.name
        };
        
        var objects = Utility.OverlapSphere(transform.position, _DamageRadius);
        
        MessageDispatcher.Send(message, objects);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKM : MonoBehaviour {
    [SerializeField]
    private AudioClip _ShotSound;

    [SerializeField]
    private float _Range;

    [SerializeField]
    private Vector3 _DefaultPosition;

    [SerializeField]
    private Vector3 _DefaultRotation;

    [SerializeField]
    private Vector3 _ReadyPosition;

    [SerializeField]
    private Vector3 _ReadyRotation;

    [SerializeField]
    private Rigidbody _Rocket;

    private EllipsoidParticleEmitter _Sparks;
	// Use this for initialization
	void Start () {
        _Sparks = gameObject.GetComponentInChildren<EllipsoidParticleEmitter>();
        _Sparks.emit = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1"))
        {
            _Sparks.Emit();
            //audio.Play(); TODO
            /*
            if (Physics.Raycast(transform.position, fwd, out hit))
            {
                if (hit.transform.tag == "Enemy" && hit.distance < _Range)
                {
                    Debug.Log("Trafiony przeciwnik");

                }
                else if (hit.distance < _Range)
                {
                    Debug.Log("Trafiona Sciana");
                }
            } //kod dla karabinu*/ 
            Rigidbody rocket = Instantiate(_Rocket, transform.position, transform.rotation) as Rigidbody;
            rocket.AddForce(transform.TransformDirection(Vector3.forward * 1000));
        }
        if (Input.GetButtonDown("Fire2"))
        {
            transform.localPosition = _ReadyPosition;
            transform.localEulerAngles = _ReadyRotation;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            transform.localPosition = _DefaultPosition;
            transform.localEulerAngles = _DefaultRotation;
        }
    }
}

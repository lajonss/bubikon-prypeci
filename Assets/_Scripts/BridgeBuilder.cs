using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class BridgeBuilder : MonoBehaviour
{
    [SerializeField] private GameObject _BridgeFragment;

    [SerializeField] private GameObject _LeftBridgeEnd;
    [SerializeField] private GameObject _RightBridgeEnd;
    [SerializeField] private GameObject _FPSContoller;
    [SerializeField] private GameObject _Spawner;
    [SerializeField] private GameObject _Light;
    [SerializeField] private float _FightLightIntensity;

    private Boolean isBuiding = true;
    private Camera _Camera;
    private List<GameObject> _Fragments;

    void Start()
    {
        _Camera = GetComponent<Camera>();
        _Fragments = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isBuiding)
        {
            var ray = _Camera.ScreenPointToRay(Input.mousePosition);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Bridge"))
                {
                    if (hit.normal.y == 0 && Math.Sign(hit.normal.x) != Math.Sign(hit.normal.z))
                    {
                        var t = hit.collider.transform;
                        var position = new Vector3(t.position.x + t.localScale.x * hit.normal.x, t.position.y,
                            t.position.z + t.localScale.z * hit.normal.z);
                        var fragment = Instantiate(_BridgeFragment, position, t.rotation);
                        fragment.transform.localScale = t.localScale * 0.995f;

                        var bunkrowNieMa = fragment.GetComponent<HingeJoint>();
                        bunkrowNieMa.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
                        if (hit.normal.x > 0)
                        {
                            bunkrowNieMa.anchor = new Vector3(-0.5f, -0.5f, 0f);
                        }
                        else
                        {
                            bunkrowNieMa.anchor = new Vector3(0.5f, -0.5f, 0f);
                        }
                        foreach (var f in _Fragments)
                        {
                            if (Vector3.Distance(f.transform.position, position) <=
                                fragment.transform.localScale.z * 1.5f)
                            {
                                if (!hit.collider.gameObject.Equals(f))
                                {
                                    var joint = fragment.AddComponent<HingeJoint>();
                                    var type = joint.GetType();
                                    var fields = type.GetFields();
                                    foreach (var field in fields)
                                    {
                                        field.SetValue(joint, field.GetValue(bunkrowNieMa));
                                    }
                                    joint.connectedBody = f.GetComponent<Rigidbody>();
                                    joint.anchor = new Vector3(1f - bunkrowNieMa.anchor.x, bunkrowNieMa.anchor.y,
                                        bunkrowNieMa.anchor.z);
                                }
                            }
                        }
                        _Fragments.Add(fragment);
                    }
                }
            }
            if (IsValid())
            {
                _FPSContoller.SetActive(true);
                _FPSContoller.GetComponentInChildren<Camera>().enabled = true;
                _Spawner.GetComponent<GameEnemySpawner>().startSpawning();
                isBuiding = false;
                _Light.GetComponent<Light>().intensity = _FightLightIntensity;
                EnablePhysicsOnBridge();
            }
        }
    }

    public void EnablePhysicsOnBridge()
    {
        foreach (var f in _Fragments)
        {
            var component = f.GetComponent<Rigidbody>();
            component.isKinematic = false;
            component.WakeUp();
        }
    }

    public bool IsValid()
    {
        return _Fragments.Count == 5;
    }
}
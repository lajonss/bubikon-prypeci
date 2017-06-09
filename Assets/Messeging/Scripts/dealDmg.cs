using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dealDmg : MonoBehaviour {
    #region Inspector Variables
    [SerializeField]
    private float _Damage = 100.0f;
    [SerializeField]
    private float _DamageRadius = 1.0f;
    #endregion Inspector Variables

    #region Overrided Methods
    void Update () {
        var message = new MessageTypes.Damage()
        {
            // Why this works
            // But Value = _Damage * Time.deltaTime; Send always 1 dmg instead of 100?
            Value = _Damage * Time.deltaTime,
            Sender = gameObject.GetInstanceID()
        };
        
        var objects = Utility.OverlapSphere(transform.position, _DamageRadius);

        MessageDispatcher.Send(message, objects);
    }
    #endregion Overrided Methods
}

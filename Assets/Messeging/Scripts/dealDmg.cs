using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dealDmg : MonoBehaviour {
    #region Inspector Variables
    [SerializeField]
    private float _Damage = 100.0f;
    [SerializeField]
    private float _DamageRadius = 1.0f;
    [SerializeField]
    private float _attackDeley = 1.0f;
    [SerializeField]
    private string _target = "";

    private float attackCounter = 0;
    #endregion Inspector Variables

    #region Overrided Methods
    void Update () {
        attackCounter -= Time.deltaTime;
        if (attackCounter <= 0)
        {
            var message = new MessageTypes.Damage()
            {
                // Why this works
                // But Value = _Damage * Time.deltaTime; Send always 1 dmg instead of 100?
                Value = _Damage,
                Sender = gameObject.tag,
                Target = _target
            };

            var objects = Utility.OverlapSphere(transform.position, _DamageRadius);

            MessageDispatcher.Send(message, objects);
            attackCounter = _attackDeley;
        }
    }
    #endregion Overrided Methods
}

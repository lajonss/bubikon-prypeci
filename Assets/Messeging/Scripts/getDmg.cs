using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getDmg : MonoBehaviour {

    [SerializeField]
    public float hp = 20.0f;

    private void Damage(MessageTypes.Damage message)
    {
        
        if (message.Target == gameObject.tag)
        {
            hp -= message.Value;
        }
    }
}

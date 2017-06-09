using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTypes : MonoBehaviour {


    #region Health
    public class Damage
    {
        public float Value;
        public string Sender;
        public string Target;
    }
    #endregion Health
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUser : MonoBehaviour
{
    [SerializeField] private GameObject detector;
    private Component dmg;

    void Start()
    {
        dmg = GetComponent<getDmg>();
    }
	
	void Update () {
	    if (dmg != null && ((getDmg) dmg).hp <= 0)
	    {
	        if (detector != null)
	        {
	            var component = detector.GetComponent<EndGameDetector>();
	            if (component != null)
	            {
	                component.EndGame();
	            }
            }
	    }
	}
}

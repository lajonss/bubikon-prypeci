using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameDetektor : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Bridge"))
        {
            var startTime = PlayerPrefs.GetFloat("StartTime");
            var endTime = System.DateTime.Now.Hour * 100 + System.DateTime.Now.Minute * 10 + System.DateTime.Now.Second;
            var score = endTime - startTime;
            GUI.Label(new Rect(0, 20, 500, 200), score.ToString());
        }
    }
}

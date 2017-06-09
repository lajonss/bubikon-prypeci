using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameDetector : MonoBehaviour {
    [SerializeField]
    private GameObject _BuildingCamera;
    [SerializeField]
    private GameObject _FPSController;

    private bool _GameEnd = false;
    private float _Score = 0f;
    private Camera _NewCamera;

    private void Start()
    {
        _NewCamera = _BuildingCamera.GetComponent<Camera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Bridge") || other.gameObject.tag.Equals("Player"))
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        if (!_GameEnd)
        {
            var startTime = PlayerPrefs.GetFloat("StartTime");
            var endTime = System.DateTime.Now.Hour * 100 + System.DateTime.Now.Minute * 10 + System.DateTime.Now.Second;
            _Score = endTime - startTime;
            _FPSController.SetActive(false);
            _NewCamera.enabled = true;
            _GameEnd = true;
        }
    }

    private void OnGUI()
    {
        if(_GameEnd)
        {
            GUI.Label(new Rect(0, 20, 500, 200), _Score.ToString());
        }
    }
}

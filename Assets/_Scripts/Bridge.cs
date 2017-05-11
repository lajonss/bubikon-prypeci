using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {
    [SerializeField]
    private float _InitialHealth = 100f;

    [SerializeField]
    private Texture2D _HealthTexture;

    [SerializeField]
    private int _HealthBarHeightPercentage = 10;

    private float _CurrentHealth;
    private float _HealthBarHeight;

	void Start () {
        _CurrentHealth = _InitialHealth;
	}

    private void Awake() {
        _HealthBarHeight = 0.01f * _HealthBarHeightPercentage * Screen.height;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, (_CurrentHealth / _InitialHealth) * Screen.width, _HealthBarHeight), _HealthTexture);
    }
}

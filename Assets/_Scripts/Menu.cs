using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private Texture GameLogo;
    private float ButtonWidth = 300;
    private float ButtonHeight = 60;
    private float ButtonMargin = 20;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 20, 500, 200), GameLogo);
        if (GUI.Button(new Rect(300, 300, ButtonWidth, ButtonHeight), "Przystąp do zerówki"))
        {
            Application.LoadLevel("main");
        }
        if (GUI.Button(new Rect(300, 300 + (ButtonHeight + ButtonMargin), ButtonWidth, ButtonHeight), "Dziekanka..."))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

}
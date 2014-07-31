using UnityEngine;
using System.Collections;

public class RestartButton: MonoBehaviour {
  void OnGUI() {
    GUI.Label(new Rect(Screen.width / 2 - 30, 80, 100, 30), "Game Over");
    if(GUI.Button(new Rect(Screen.width / 2 - 50,
                           Screen.height / 2, 100, 50), "Main Menu")) {
      Application.LoadLevel("Opening");
    }

  }
}
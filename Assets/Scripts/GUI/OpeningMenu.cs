using UnityEngine;
using System.Collections;

public class OpeningMenu: MonoBehaviour {
  void Start() {
    // Clear out score from previous game.
    GameController.control.Reset();
  }

  void OnGUI() {
    GUI.Label(new Rect(Screen.width / 2f - 25,
                       80f,
                       Screen.width * 0.75f,
                       Screen.height / 4f),
              "Okeanos");
    if(GUI.Button(new Rect(Screen.width / 2f - (Screen.width * 0.75f) / 2f,
                           Screen.height / 2f,
                           Screen.width * 0.75f,
                           Screen.height / 8f),
                  "Start")) {
      Application.LoadLevel("Level01");
    }

  }
}
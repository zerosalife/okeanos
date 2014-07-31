using UnityEngine;
using System.Collections;

public class OpeningMenu: MonoBehaviour {
  void OnGUI() {
    GUI.Label(new Rect(Screen.width / 2 - 30, 80, 100, 30), "Okeanos");
    if(GUI.Button(new Rect(Screen.width / 2 - 50,
                           Screen.height / 2, 100, 50), "Start")) {
      Application.LoadLevel("Level01");
    }

  }
}